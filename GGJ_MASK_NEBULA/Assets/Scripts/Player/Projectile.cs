using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    private Rigidbody2D _rb;
    private float _direction = 1f;
    private bool _hasFired = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(float dir)
    {
        _direction = dir;
        _hasFired = true;
        
        if (_direction < 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (!_hasFired) return;
        _rb.linearVelocity = new Vector2(speed * _direction, 0);
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignorar al jugador 
        if (collision.CompareTag("Player")) return;

        if (collision.isTrigger && !collision.GetComponent<Enemy>()) return;

        if (collision.TryGetComponent(out Enemy enemyScript))
        {
            enemyScript.TakeDamage(); // ¡Muere!
            return;
        }

        Destroy(gameObject);
    }
}