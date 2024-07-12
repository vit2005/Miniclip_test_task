using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHolder : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    private int _hp = 100;
    public int HP => _hp;

    public Action<float> DamagedAction;
    public Action<HealthHolder> DestroyedAction;
    public Action RevivedAction;

    public void Awake()
    {
        _hp = _maxHp;
    }

    public void SetMaxHealth(int value, bool setHealth = true)
    {
        _maxHp = value;
        if (setHealth) _hp = _maxHp;
    }

    public void Damage(int value)
    {
        _hp -= value;

        if (_hp <= 0)
        {
            _hp = 0;
            DestroyedAction?.Invoke(this);
        }
        DamagedAction?.Invoke((float)_hp/_maxHp);
    }

    public void Revive()
    {
        _hp = _maxHp;
        RevivedAction?.Invoke();
    }
}
