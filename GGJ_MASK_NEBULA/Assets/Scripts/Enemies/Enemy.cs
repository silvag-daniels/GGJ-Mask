using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 2f;
    private Transform target;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) target = playerObj.transform;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null) player.Die(); 
        }
    }
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damage) 
    {
        TakeDamage();
    }
}