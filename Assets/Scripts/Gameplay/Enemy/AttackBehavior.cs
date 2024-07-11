using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : IUpdatable
{
    private BulletSpawner _spawner;

    public void Init(BulletSpawner spawner)
    {
        _spawner = spawner;
    }

    public void OnUpdate()
    {
        _spawner.OnUpdate();
    }
}
