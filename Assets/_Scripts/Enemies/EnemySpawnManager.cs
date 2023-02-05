using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private float timeBetweenSpawns;

    private List<EnemySpawner> enemySpawners;
    private float timeElapsed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        enemySpawners = new List<EnemySpawner>();
    }

    public void AddSpawner(EnemySpawner spawner)
    {
        enemySpawners.Add(spawner);
    }

    private void SpawnEnemy()
    {
        int rng = Random.Range(0, enemySpawners.Count);

        EnemyController enemy = Instantiate(enemyPrefab, enemySpawners[rng].transform.position, enemyPrefab.transform.rotation);
        //enemy.transform.Rotate(Vector3.up, 45f);

        rng = Random.Range(1, 4);
        ElementalType element = (ElementalType)rng;

        enemy.Element = element;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeBetweenSpawns)
        {
            return;
        }

        SpawnEnemy();
        timeElapsed -= timeBetweenSpawns;
    }
}
