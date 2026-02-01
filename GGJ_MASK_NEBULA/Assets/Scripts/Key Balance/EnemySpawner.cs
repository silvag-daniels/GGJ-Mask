using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnRate = 2f;
    private GameObject[] enemyPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.SetActive(false);
            enemyPool[i] = enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate -= Time.deltaTime;
        if(spawnRate < 0)
        {
            spawnRate = 2f;
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(true);
                    break;
                }
            }
        }
    }
}
