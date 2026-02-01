using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Autodestrucción para optimizar memoria
        // Se mueve en su derecha local (que ya rotamos en el Guardián)
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reinicio inmediato usando SceneManagement nativo
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (!other.CompareTag("Enemy")) // Ignora colisiones con otros enemigos
        {
            Destroy(gameObject);
        }
    }
}