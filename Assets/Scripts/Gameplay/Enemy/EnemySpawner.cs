using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IUpdatable
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform mainTarget;

    public int enemiesToSpawn = 10;
    public bool allEnemiesSpawned = false;

    public List<EnemyController> enemies = new List<EnemyController>();

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time;
    }

    public void OnUpdate()
    {
        if (!allEnemiesSpawned && Time.time >= nextSpawnTime)
        {
            if (SpawnEnemy()) nextSpawnTime = Time.time + spawnInterval;
        }

        foreach (EnemyController e in enemies) {
            e.OnUpdate();
        }
    }

    private bool SpawnEnemy()
    {
        if (enemiesToSpawn == 0)
        {
            allEnemiesSpawned = true;
            return false;
        }

        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        EnemyController enemy = enemyPool.GetEnemy();
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.transform.rotation = spawnPoints[spawnIndex].rotation;
        enemy.Init(bulletPool, mainTarget);
        enemy.GetComponent<HealthHolder>().DestroyedAction += OnEnemyDeath;
        enemies.Add(enemy);
        enemiesToSpawn--;
        return true;
    }

    private void OnEnemyDeath(HealthHolder healthHolder)
    {
        enemies.Remove(healthHolder.GetComponent<EnemyController>());
    }
}
