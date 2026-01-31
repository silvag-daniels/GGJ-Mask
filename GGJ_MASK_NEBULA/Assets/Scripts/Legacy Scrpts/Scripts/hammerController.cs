using UnityEngine;

public class hammerController : MonoBehaviour
{
    public float arcHeight = 0.5f;
    public float speed = 1f;    
    private Vector3 startPoint;
    private Vector3 targetPoint;
    private float elapsedTime = 0f;
    private float travelTime;

    void Start()
    {
        GameObject player = GameObject.Find("Mario");
        if (player != null)
        {
            startPoint = transform.position;
            targetPoint = player.transform.position;
            travelTime = Vector3.Distance(startPoint, targetPoint) / speed;
            if(targetPoint.x < startPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            Debug.LogError("Player not found!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / travelTime);

            Vector3 linear = Vector3.Lerp(startPoint, targetPoint, t);

            float heightOffset = arcHeight * 4f * (t - t * t);

            transform.position = new Vector3(
                linear.x,
                linear.y + heightOffset,
                linear.z
            );
        }
        else
        {
            Destroy(gameObject); // Se destruye al llegar
        }
    }
}