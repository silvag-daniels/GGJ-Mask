using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject doorReference;
    public GameObject Key;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BasePlayer")
        {
            collision.gameObject.SendMessage("StartLabelControl");
            doorReference.SendMessage("Open");
            Destroy(Key);
        }
    }

    void Respawn()
    {
        Key.transform.position = spawnPoint.position;
    }
}
