using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyConfig : Config<EnemyConfig>
{
    public float moveSpeed = 1f;
    public float health = 1f;
    public float radius = 1f;
    public float attackSpeed = 1f;
    public int attackPower = 1;
    public LayerMask layerMask;
}