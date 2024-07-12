using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "ScriptableObjects/TowerConfig")]
public class TowerConfig : Config<TowerConfig>
{
    public int health = 3;
    public float radius = 1f;
    public float attackSpeed = 1f;
    public int attackPower = 1;
    public LayerMask layerMask;
}
