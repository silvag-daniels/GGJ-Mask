using System.ComponentModel.Design;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    public GameObject manager;
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
        if (other.tag == "Player")
        {
            manager.SendMessage("StartMiniGame");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                //Activar Enemy Script
                child.gameObject.GetComponent<Enemy>().enabled = true;
            }
        }
    }
}
