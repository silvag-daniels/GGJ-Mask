using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chumpController : MonoBehaviour
{
    public float moveDistance = 1f; 
    public float moveDuration = 0.5f; 
    public float waitTime = 1f; 

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; 
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(transform.position + Vector3.up * moveDistance, moveDuration));
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(MoveToPosition(initialPosition, moveDuration));
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.position = targetPosition;
    }
}
