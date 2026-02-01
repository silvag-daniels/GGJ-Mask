using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tortugaHurth : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Transform tortuga;

    void Awake(){
        _rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private bool dead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHit")) 
        {
            if (!dead) 
            {
                collision.SendMessageUpwards("jumpKill");
                gameObject.SendMessageUpwards("animateKill");
                dead = true;
            }
            else 
            {
                GameObject player = GameObject.Find("Mario");
                Transform playerTransform = player.transform;  // Obtener el Transform del jugador
                float direction = playerTransform.localScale.x > 0f ? -1f : 1f;

                // Comparar las posiciones Y
                TortugaDTO tortugaDTO = new TortugaDTO(direction, playerTransform.position.y > tortuga.position.y);

                transform.parent.SendMessageUpwards("MoveRigidbody", tortugaDTO);
                gameObject.SendMessageUpwards("animateKill");
            }
        }
    }
}