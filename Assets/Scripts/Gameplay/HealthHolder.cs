using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHolder : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    private int _hp = 100;
    public int HP => _hp;

    public Action DamagedAction;
    public Action DestroyedAction;
    public Action RevivedAction;

    public void Awake()
    {
        _hp = _maxHp;
    }

    public void Damage(int value)
    {
        _hp -= value;
        DamagedAction?.Invoke();

        if (_hp <= 0)
        {
            _hp = 0;
            DestroyedAction?.Invoke();
        }
    }

    public void Revive()
    {
        _hp = _maxHp;
        RevivedAction?.Invoke();
    }
}
