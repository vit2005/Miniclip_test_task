using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : IUpdatable
{
    protected BulletSpawner spawner;
    protected Rigidbody source;

    public void Init(Rigidbody source, BulletSpawner spawner)
    {
        this.spawner = spawner;
        this.source = source;
    }

    public virtual void OnUpdate()
    {
        spawner.OnUpdate();
        if (source != null) source.velocity = Vector3.Lerp(source.velocity, Vector3.zero, 0.5f);
    }
}
