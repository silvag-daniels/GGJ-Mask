using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHit")) {
            collision.SendMessageUpwards("jumpKill");
			gameObject.SendMessageUpwards("animateKill");
		}
	}
}
