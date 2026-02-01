using UnityEngine;

public class GuardianEnemy : Enemy
{
    [Header("Guardian Config")]
    public Transform player; 
    public GameObject projectilePrefab;
    public float detectionRange = 10f;
    public float fireRate = 2f;
    private float nextFireTime;

    // ELIMINADO 'override' y 'base.Start()' porque el padre es privado
    private void Start()
    {
        // Buscamos al jugador manualmente ya que no podemos usar la lógica del padre
        if (player == null) 
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    // ELIMINADO 'override'. Al definir Update aquí, Unity usará este en vez del de Enemy.cs
    private void Update()
    {
        if (player == null) return;

        // Si el jugador está cerca...
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            HandleRotation();
            
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void HandleRotation()
    {
        // Voltea el sprite para mirar al jugador
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void Shoot()
    {
        if (!projectilePrefab) return;

        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}