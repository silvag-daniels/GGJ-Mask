using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Función pública para que otros objetos (como la bala) puedan llamarla
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}