using System;
using UnityEngine;

public class MoveBehavior : IUpdatable
{
    private Transform _source;
    private Transform _target;
    private float _speed;
    private float _radius;
    private int _layerMask;
    private Rigidbody _rb;

    private bool isMoving = false;

    public Action<HealthHolder> TargetSpotted;
    

    public void Init(Transform source, Transform target, float speed, float radius, int layerMask)
    {
        _source = source;
        _rb = _source.GetComponent<Rigidbody>();
        _target = target;
        _speed = speed;
        _radius = radius;
        _layerMask = layerMask;
    }

    public void StartMoving()
    {
        _rb.velocity = (_target.position - _source.position).normalized * _speed;
        isMoving = true;
    }

    public void StopMoving()
    {
        _rb.velocity = Vector3.zero;
        isMoving = false;
    }

    public void OnUpdate()
    {
        //if (isMoving) _rb.velocity = (_target.position - _source.position).normalized * _speed;
        //else _rb.velocity = Vector3.zero;

        var colliders = Physics.OverlapSphere(_source.position, _radius, _layerMask);
        if (colliders.Length > 0)
        {
            TargetSpotted?.Invoke(colliders[0].gameObject.GetComponent<HealthHolder>());
            StopMoving();
        }
    }
}
