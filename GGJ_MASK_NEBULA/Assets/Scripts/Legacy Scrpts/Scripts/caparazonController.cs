using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caparazonController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _facingRight = true;
    public LayerMask groundLayer;
    public float wallAware;
    public GameObject hit_box;
    public GameObject hurt_box;
    public float horizontalVelocity = 1.5f;
    private bool moving = false;
    private BoxCollider2D hit_collider;
    private BoxCollider2D hurt_collider;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        hit_collider = hit_box.GetComponent<BoxCollider2D>();
        hurt_collider = hurt_box.GetComponent<BoxCollider2D>();
    }

    public void MoveRigidbody(TortugaDTO tortugaDTO){
        if (_rigidbody != null)
        {
            if(tortugaDTO.getArriba()){
                if(moving){
                    _rigidbody.linearVelocity = Vector2.zero;
                    _rigidbody.Sleep();
                    moving = false;
                    return;
                }
            }
            _rigidbody.WakeUp();
            float vel = horizontalVelocity;
            Debug.Log(tortugaDTO.getDirection());
            _facingRight = tortugaDTO.getDirection() > 0f; 
            _rigidbody.linearVelocity = new Vector2(vel * tortugaDTO.getDirection(), _rigidbody.linearVelocity.y); 
            moving = true;
        }
    }

    private void FixedUpdate()
    {
        moving = (_rigidbody.linearVelocity.x > (horizontalVelocity - 0.1) && _rigidbody.linearVelocity.x < (horizontalVelocity + 0.1)) || (_rigidbody.linearVelocity.x > (horizontalVelocity * -1 - 0.1) && _rigidbody.linearVelocity.x < (horizontalVelocity * -1 + 0.1));
        if (moving){
            float velocity = horizontalVelocity;
            if (!_facingRight){
                velocity = horizontalVelocity * -1f;
            }
            _rigidbody.linearVelocity = new Vector2(velocity, _rigidbody.linearVelocity.y);
        }else{
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        Vector2 direction = Vector2.right;
        if (_facingRight == false)
        {
            direction = Vector2.left;
        }
        if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
        {
            Flip();
        }
        if (moving){   
           if (!hit_collider.enabled){
                StartCoroutine(EsperarYEjecutar());
            }
        }else{
            hit_collider.enabled = false;
        }
    }

    IEnumerator EsperarYEjecutar()
{
    yield return new WaitForSeconds(0.25f);
    hit_collider.enabled = true;
    hurt_collider.size = new Vector2(hurt_collider.size.x, 0.04f);
    hurt_collider.offset = new Vector2(hurt_collider.offset.x, 0.07f);
}


    private void Flip()
    {
        _facingRight = !_facingRight;
    }
}
