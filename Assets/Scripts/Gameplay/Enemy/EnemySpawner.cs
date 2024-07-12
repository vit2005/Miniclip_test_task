using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform mainTarget; 

    private List<EnemyController> enemies = new List<EnemyController>();

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        foreach (EnemyController e in enemies) {
            e.OnUpdate();
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        EnemyController enemy = enemyPool.GetEnemy();
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.transform.rotation = spawnPoints[spawnIndex].rotation;
        enemy.Init(bulletPool, mainTarget);
        enemy.GetComponent<HealthHolder>().DestroyedAction += OnEnemyDeath;
        enemies.Add(enemy);
    }

    private void OnEnemyDeath(HealthHolder healthHolder)
    {
        enemies.Remove(healthHolder.GetComponent<EnemyController>());
    }
}
