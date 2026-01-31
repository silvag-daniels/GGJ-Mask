using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class playerControleer : MonoBehaviour
{
    public float longIdleTime = 5f;
	public float speed = 2.5f;
	public float jumpForce = 2.5f;
	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;
	public TextMeshProUGUI coinText;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private float _longIdleTimer;
	private Vector2 _movement;
	private bool _facingRight = false;
	private bool _isGrounded;
	private bool dead = false;
    private int health = 1;
	private int coins = 0;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
		if(dead) return;

		float horizontalInput = Input.GetAxisRaw("Horizontal");
		Debug.Log("Horizontal input: " + horizontalInput);
		_movement = new Vector2(horizontalInput, 0f);
		if ((horizontalInput < 0f && _facingRight) || (horizontalInput > 0f && !_facingRight)) {
			Flip();
		}
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (Input.GetButtonDown("Jump") && _isGrounded) {
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
		// Wanna Attack?
		/*if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false) {
			_movement = Vector2.zero;
			_rigidbody.velocity = Vector2.zero;
			_animator.SetTrigger("attack");
		}*/
    }

    void FixedUpdate()
	{
		if(!dead){
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.linearVelocity = new Vector2(horizontalVelocity, _rigidbody.linearVelocity.y);
		}
	}

    void LateUpdate()
	{
		_animator.SetBool("idle", _movement == Vector2.zero);
		_animator.SetBool("isGrounded", _isGrounded);
		_animator.SetFloat("verticalVelocity", _rigidbody.linearVelocity.y);
		// Long Idle
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			_longIdleTimer += Time.deltaTime;
			if (_longIdleTimer >= longIdleTime) {
				_animator.SetTrigger("longIdle");
			}
		} else {
			_longIdleTimer = 0f;
		}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}

	public void die(){
		dead = true;
	}

	public void coinPickup(){
		coins++;
		if(coins < 10){
			coinText.text = "000" + coins.ToString();
		}else if(coins < 100){
			coinText.text = "00" + coins.ToString();
		}else if(coins < 1000){
			coinText.text = "0" + coins.ToString();
		}else{
			coinText.text = coins.ToString();
		}
	}
}
