using UnityEngine;
using System.Collections;

public class turtleHammerScript : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask groundLayer;   // Para detectar paredes
    public LayerMask playerLayer;   // Para detectar jugador
    public float wallAware = 0.5f;  // Distancia de detección de pared
    public float rangeView = 2f;    // Rango para detectar al jugador
    public float deadScale = 0f;
    public float cooldown = 1f;   // Tiempo de espera entre ataques
    public GameObject hammerPrefab; // Prefab del martillo

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _facingRight;
    private bool onRange = false;
    private bool killed = false;
    private GameObject player;
    private bool inCooldown = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _facingRight = transform.localScale.x > 0f;
    }

    void Update()
    {
        Vector2 direccion = _facingRight ? Vector2.right : Vector2.left;

        onRange = false;

        if (player != null)
        {
            Vector2 enemyPos = transform.position;
            Vector2 playerPos = player.transform.position;

            float distance = Vector2.Distance(enemyPos, playerPos);

            if (distance <= rangeView &&
                ((!_facingRight && playerPos.x < enemyPos.x) ||
                (_facingRight && playerPos.x > enemyPos.x)))
            {
                Vector2 directionToPlayer = (playerPos - enemyPos).normalized;
                enemyPos += _facingRight ? Vector2.right * 0.15f : Vector2.left * 0.15f;
                enemyPos.y += 0.075f;
                int excludedLayer = LayerMask.NameToLayer("Ignore Raycast");
                int layerMask = ~(1 << excludedLayer);
                RaycastHit2D hit = Physics2D.Raycast(enemyPos, directionToPlayer, rangeView, layerMask);

                Debug.DrawRay(enemyPos, directionToPlayer * rangeView, Color.cyan);

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    onRange = true;
                    Debug.Log("Jugador detectado con visión clara y en dirección válida");
                }
                else
                {
                    Debug.Log("Dandole a " + hit.collider?.name);
                }
            }
        }


        // Ejecutar animación de ataque
        _animator.SetBool("attack", onRange);

        if (onRange && !killed)
        {
            // Si el jugador está en rango, detener el movimiento
            _rigidbody.linearVelocity = Vector2.zero;
            StartCoroutine(throwHammer());
        }
        else if (!killed)
        {
            // Detectar paredes para hacer Flip
            Debug.DrawRay(transform.position, direccion * wallAware, Color.red);
            if (Physics2D.Raycast(transform.position, direccion, wallAware, groundLayer))
            {
                Flip();
            }
        }
    }

    void FixedUpdate()
    {
        if (!killed && !onRange)
        {
            float horizontalVelocity = _facingRight ? speed : -speed;
            _rigidbody.linearVelocity = new Vector2(horizontalVelocity, _rigidbody.linearVelocity.y);
        }
        else if (killed || onRange)
        {
            _rigidbody.linearVelocity = new Vector2(0f, _rigidbody.linearVelocity.y);
        }
    }

    public void animateKill()
    {
        killed = true;
        if (deadScale != 0f)
        {
            transform.localScale = new Vector3(transform.localScale.x, deadScale, transform.localScale.z);
        }
        _animator.SetTrigger("kill");
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
    
    private IEnumerator throwHammer() {
        if(!killed && !inCooldown) {
            inCooldown = true;
            Instantiate(hammerPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown); // Espera a que la animación termine
            inCooldown = false;
        }
    }
}