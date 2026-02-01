using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float longIdleTime = 5f;
	public float speed = 2.5f;
	public float jumpForce = 2.5f;
	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private float _longIdleTimer;
	private Vector2 _movement;
	private bool _facingRight = false;
	private bool _isGrounded;
	private bool dead = false;
    private int health = 1;
	// private int coins = 0;

	[Header("Combat System")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab; 
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		_movement = new Vector2(horizontalInput, 0f);
		if ((horizontalInput < 0f && _facingRight) || (horizontalInput > 0f && !_facingRight)) {
			Flip();
		}
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (Input.GetButtonDown("Jump") && _isGrounded) {
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
		// Wanna Attack?
		if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

	// private void OnDrawGizmos()
	// {
	// 	Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	// }

	private void Shoot()
    {
        if (firePoint == null || bulletPrefab == null) return;

        // Instanciamos la bala en la posición del FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        float direction = _facingRight ? 1f : -1f; 
        

        // Obtenemos el script de la bala y le pasamos la velocidad
        Projectile projectileScript = bullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction); 
        }
    }

    void FixedUpdate()
	{
		if(!dead){
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.linearVelocity = new Vector2(horizontalVelocity, _rigidbody.linearVelocity.y);
		}
	}

    private IEnumerator animateDead()
    {
        _rigidbody.linearVelocity = new Vector2(0, 3);
		yield return new WaitForSeconds(1.0f);
		_rigidbody.linearVelocity = new Vector2(0, -3);
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

	public void damage(){
		health--;
		if(health < 1){
			_animator.SetTrigger("kill");
		}
	}

	public void jumpKill(){
		_rigidbody.AddForce(Vector2.up * 4.0f, ForceMode2D.Impulse);
	}
	public void Die()
{	
	dead = true;
    Debug.Log("Game Over: El jugador ha muerto.");
	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Destroy(gameObject); 
}
}
