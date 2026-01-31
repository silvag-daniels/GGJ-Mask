using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeViewController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        gameObject.SendMessageUpwards("UpdateOnRange", true);
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        gameObject.SendMessageUpwards("UpdateOnRange", false);
    }
}

}
