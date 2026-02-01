using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingTurtleController : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float jumpForce;
    public GameObject tortugaPrefab;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool canJump = true;
    private int saltos = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) && canJump && (saltos < 3))
        {
            StartCoroutine(SaltarCadaXSegundos(0.25f));
        }else if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)){
            saltos = 0;
        }
    }

    IEnumerator SaltarCadaXSegundos(float delay)
    {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        saltos++;
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true; 
    }

    private void animateKill(){
        Vector3 position = transform.position;
        StartCoroutine(InstantiateTortuga(position));
    }

    IEnumerator InstantiateTortuga(Vector3 position){
        _animator.SetTrigger("kill");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        GameObject tortuga = tortugaPrefab;
        tortuga.transform.localScale = this.transform.localScale;
        Instantiate(tortuga, position, Quaternion.identity);
    }
}
