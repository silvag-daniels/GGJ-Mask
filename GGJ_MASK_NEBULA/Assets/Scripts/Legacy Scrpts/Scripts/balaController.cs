using UnityEngine;

public class balaController : MonoBehaviour
{
    public float lifeTime = 2f;
    private Rigidbody2D rb;
    private cannonController poolRef;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Invoke(nameof(DisableAndReturn), lifeTime);
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    public void SetPoolReference(cannonController pool)
    {
        poolRef = pool;
    }

    public void animateKill()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        transform.localScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z);
        Invoke(nameof(DisableAndReturn), 0.1f); // Espera medio segundo antes de desactivar
    }

    private void DisableAndReturn()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        this.gameObject.SetActive(false);
        poolRef?.ReturnBullet(this.gameObject);
    }
}