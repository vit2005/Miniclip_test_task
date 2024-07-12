using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TowerAttackBehavior : AttackBehavior
{
    private Transform _source;
    private float _speed;
    private float _radius;
    private int _layerMask;

    public void Init(Transform source, float speed, float radius, int layerMask, BulletSpawner spawner)
    {
        this.spawner = spawner;
        _source = source;
        _speed = speed;
        _radius = radius;
        _layerMask = layerMask;
    }

    public override void OnUpdate()
    {
        var colliders = Physics.OverlapSphere(_source.position, _radius, _layerMask);
        if (colliders.Length > 0)
        {
            spawner.SetTarget(colliders[0].transform);
            base.OnUpdate();
        }
    }
}
