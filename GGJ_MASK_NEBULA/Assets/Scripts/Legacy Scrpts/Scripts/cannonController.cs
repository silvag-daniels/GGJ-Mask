using System.Collections.Generic;
using UnityEngine;

public class cannonController : MonoBehaviour
{
    public GameObject balaPrefab;
    public float fireRate = 0.25f;
    public float balaSpeed = 1f;
    public int poolSize = 10;
    private Queue<GameObject> bulletPool;
    public bool left = true;
    public bool right = true;
    private float leftCooldown = 0f;
    private float rightCooldown = 0f;

    void Awake()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(balaPrefab);
            bullet.GetComponent<balaController>().SetPoolReference(this); // 👈 Asigna referencia
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    void Update()
    {
        if (left)
        {
            leftCooldown -= Time.deltaTime;
            if (leftCooldown <= 0f)
            {
                FireBullet(Vector2.left);
                leftCooldown = 1f / fireRate;
            }
        }

        if (right)
        {
            rightCooldown -= Time.deltaTime;
            if (rightCooldown <= 0f)
            {
                FireBullet(Vector2.right);
                rightCooldown = 1f / fireRate;
            }
        }
    }

    private void FireBullet(Vector2 direction)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f);
            bullet.transform.position = transform.position + Vector3.up * 0.09f + Vector3.right * direction.x * 0.1f;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * balaSpeed;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}