using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthHolder))]
public class DamageVisualizer : MonoBehaviour
{
    private Material material;
    private HealthHolder healthHolder;

    private void Start()
    {
        healthHolder = GetComponent<HealthHolder>();
        healthHolder.DamagedAction += OnDamage;
        material = GetComponent<Renderer>().material;
    }

    private void OnDamage(float percentage)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, percentage);
    }
}
