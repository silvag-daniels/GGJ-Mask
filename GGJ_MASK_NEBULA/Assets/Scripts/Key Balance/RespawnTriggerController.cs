using UnityEngine;

public class RespawnTriggerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BasePlayer")
        {
            other.gameObject.SendMessage("die");
        }
        else if(other.tag == "Key" || other.tag == "Enemy")
        {
            other.gameObject.SendMessage("Respawn");
        }
    }
}
