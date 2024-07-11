using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyController enemy;
    [SerializeField] BulletPool bulletPool;
    [SerializeField] Transform mainTarget;

    public void Awake()
    {
        enemy.Init(bulletPool, mainTarget);
    }

    public void Update()
    {
        enemy.OnUpdate();
    }
}
