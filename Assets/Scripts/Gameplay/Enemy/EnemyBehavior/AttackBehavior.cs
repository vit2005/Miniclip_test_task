using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : IUpdatable
{
    protected BulletSpawner spawner;

    public void Init(BulletSpawner spawner)
    {
        this.spawner = spawner;
    }

    public virtual void OnUpdate()
    {
        spawner.OnUpdate();
    }
}
