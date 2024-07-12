using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour, IUpdatable
{
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private TowerConfig config;
    [SerializeField] private HealthHolder healthHolder;

    private TowerAttackBehavior _attackBehavior;

    public void Init(BulletPool pool)
    {
        bulletSpawner.Init(pool, config.attackSpeed, config.attackPower, config.layerMask);
        healthHolder.SetMaxHealth(config.health);

        _attackBehavior = new TowerAttackBehavior();
        _attackBehavior.Init(transform, config.attackSpeed, config.radius, config.layerMask, bulletSpawner);
    }

    public void OnUpdate()
    {
        _attackBehavior.OnUpdate();
    }
}
