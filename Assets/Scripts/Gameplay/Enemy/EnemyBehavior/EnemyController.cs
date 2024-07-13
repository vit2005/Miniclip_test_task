using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IUpdatable
{
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private EnemyConfig config;
    [SerializeField] private HealthHolder _selfHealthHolder;

    private AttackBehavior _attackBehavior;
    private MoveBehavior _moveBehavior;

    private IUpdatable _currentBehavior;

    private HealthHolder _targetHealth;

    public void Init(BulletPool pool, Transform mainTarget)
    {
        _selfHealthHolder.SetMaxHealth(config.health);
        _selfHealthHolder.DestroyedAction += OnSelfDestroyed;

        _moveBehavior = new MoveBehavior();
        _moveBehavior.Init(transform, mainTarget, config.moveSpeed, config.radius, config.layerMask);
        _moveBehavior.TargetSpotted += OnTowerSpotted;

        bulletSpawner.Init(pool, config.attackSpeed, config.attackPower, config.layerMask);

        _attackBehavior = new AttackBehavior();
        _attackBehavior.Init(gameObject.GetComponent<Rigidbody>(), bulletSpawner);

        _currentBehavior = _moveBehavior;
        _moveBehavior.StartMoving();
    }

    public void OnTowerSpotted(HealthHolder healthHolder)
    {
        _targetHealth = healthHolder;
        _targetHealth.DestroyedAction += OnTowerDestroyed;
        bulletSpawner.SetTarget(_targetHealth.transform);
        _currentBehavior = _attackBehavior;
    }

    public void OnTowerDestroyed(HealthHolder healthHolder)
    {
        _currentBehavior = _moveBehavior;
        _moveBehavior.StartMoving();
    }

    public void OnSelfDestroyed(HealthHolder healthHolder)
    {
        _currentBehavior = null;
    }

    public void StartMoving()
    {
        _moveBehavior.StartMoving();
    }

    public void StopMoving()
    {
        _moveBehavior.StopMoving();
    }

    public void OnUpdate()
    {
        _currentBehavior?.OnUpdate();
    }
}
