using System;
using TMPro;
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
    private bool isAvoiding = false;
    private int _avoidenceMask;

    public Action<HealthHolder> TargetSpotted;
    

    public void Init(Transform source, Transform target, float speed, float radius, int layerMask)
    {
        _source = source;
        _rb = _source.GetComponent<Rigidbody>();
        _target = target;
        _speed = speed;
        _radius = radius;
        _layerMask = layerMask;
        _avoidenceMask = LayerMask.GetMask("Obstacle", "Enemy");
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
        if (isMoving)
        {
            _rb.velocity = GetNormalizedDirection() * _speed;
        }
        else _rb.velocity = Vector3.zero;

        
        CheckTowers();
    }

    private void CheckTowers()
    {
        var colliders = Physics.OverlapSphere(_source.position, _radius, _layerMask);
        if (colliders.Length > 0)
        {
            TargetSpotted?.Invoke(colliders[0].gameObject.GetComponent<HealthHolder>());
            StopMoving();
        }
    }

    private Vector3 GetNormalizedDirection()
    {
        Vector3 direction = _target.position - _source.position;
        if (Physics.SphereCast(_source.position, 0.5f, direction.normalized, out _, 1f, _avoidenceMask))
        {
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
            if (isAvoiding) return -perpendicular;

            if (Physics.SphereCast(_source.position, 0.5f, perpendicular, out _, 0.5f, _avoidenceMask))
            {
                isAvoiding = true;
                return -perpendicular;
            }
            else return perpendicular;
        }
        isAvoiding = false;
        return direction.normalized;
    }
}
