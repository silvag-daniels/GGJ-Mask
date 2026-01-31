using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask groundLayer;
    public float wallAware;
    public float deadScale = 0f;
	private Rigidbody2D _rigidbody;
    //private AudioSource _audio;
    private bool _facingRight;
    private Animator _animator;
    private bool killed = false;

    void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _facingRight = transform.localScale.x > 0f;
    }

    void Update()
    {
        Debug.Log("Escala actual en Update: " + transform.localScale.x);
        Vector2 direction = Vector2.right;
		if (_facingRight == false) {
			direction = Vector2.left;
		}
        Debug.DrawRay(transform.position, direction * wallAware, Color.red);
        if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
        {
            Flip();
        }
    }

    private void FixedUpdate()
	{
        if(!killed){
            float horizontalVelocity = speed;
            if (!_facingRight) {
                horizontalVelocity = horizontalVelocity * -1f;
            }
            _rigidbody.linearVelocity = new Vector2(horizontalVelocity, _rigidbody.linearVelocity.y);
        }else{
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);
        }
		
	}

    public void kill(){
        Destroy(gameObject);
    }

    public void animateKill(){
        killed = true;
        if(deadScale != 0f){
            transform.localScale = new Vector3(transform.localScale.x, deadScale, transform.localScale.z);
        }
        _animator.SetTrigger("kill");
    }

    private void Flip()
    {
        Debug.Log("Antes del flip: " + transform.localScale.x);
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        Debug.Log("Después del flip: " + transform.localScale.x);
    }

}
