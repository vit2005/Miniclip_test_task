using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(HealthHolder))]
public class MainTowerDamageVisualizer : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private HealthHolder healthHolder;

    void Start()
    {
        healthHolder.DamagedAction += Damage;
    }

    private void Damage(float obj)
    {
        Vignette vignette;
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            float percentage = 1f - obj;
            vignette.intensity.value = 0.5f + percentage / 2f;
            vignette.smoothness.value = percentage;
        }
    }
}
