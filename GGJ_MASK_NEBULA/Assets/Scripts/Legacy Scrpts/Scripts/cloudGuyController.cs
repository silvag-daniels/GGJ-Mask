using System.Collections;
using UnityEngine;

public class cloudGuyController : MonoBehaviour
{
    public float speed = 1f;
    public Transform player;
    public float distanceThreshold = 0.1f;
    public float timeLimit = 5f;
    public float waitTime = 3f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private int estado = 0; // 0 = idle, 1 = move
    private Vector2 lastPlayerPosition;
    private bool onRange = false;
    private bool killed = false;
    private bool canMove = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!killed)
        {
            // **Siempre gira hacia el jugador, aunque esté en espera**
            FlipTowardsPlayer();

            // Si está en rango y puede moverse, iniciar el movimiento
            if (estado == 0 && onRange && canMove)
            {
                lastPlayerPosition = player.position;
                StartCoroutine(MoveToLastPlayerPosition());
            }
        }
    }

    private void FlipTowardsPlayer()
    {
        if (player == null) return;

        Debug.Log($"valor: {transform.position.x < player.position.x}");
        if (transform.position.x < player.position.x)
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        Debug.Log($"Escala: {transform.localScale.x}");
    }

    private IEnumerator MoveToLastPlayerPosition(){
        estado = 1;
        _animator.SetBool("moving", true);
        float elapsedTime = 0f;

        while (Vector2.Distance(transform.position, lastPlayerPosition) > distanceThreshold && elapsedTime < timeLimit && !killed)
        {
            // **Gira hacia el jugador mientras se mueve**
            FlipTowardsPlayer();
            transform.position = Vector2.MoveTowards(transform.position, lastPlayerPosition, speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // **Cuando termine de moverse, aseguramos la dirección final**
        FlipTowardsPlayer(); 
        estado = 0;
        _animator.SetBool("moving", false);

        // Espera antes de volver a moverse
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }

    public void UpdateOnRange(bool newOnRange)
    {
        onRange = newOnRange;
        Debug.Log($"onRange actualizado a: {onRange}");
    }

    public void animateKill()
    {
        killed = true;
        transform.localScale = new Vector3(transform.localScale.x, -0.65f, transform.localScale.z);
        _animator.SetTrigger("kill");
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    void LateUpdate(){
        FlipTowardsPlayer();
    }
}