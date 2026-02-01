using UnityEngine;

public class RotationLabelController : MonoBehaviour
{
    public GameObject PlayerReference;
    public float rotationSpeed = 1.5f;
    private bool controlled = false;
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
        if (collision.gameObject.tag == "BasePlayer" && !controlled)
        {
            collision.gameObject.SendMessage("StartLabelControl");
            controlled = true;
        }
    }

    void Rotate(float direction)
    {
        direction = direction * -1;
        foreach(Transform child in transform)
        {
            if(child.transform.tag == "Platform")
            {
                child.transform.Rotate(new Vector3(0f, 0f, direction * rotationSpeed * Time.deltaTime).normalized);
            }
        }
    }
}
