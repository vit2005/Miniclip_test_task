using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthHolder))]
public class DestroyAnimationHandler : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HealthHolder>().DestroyedAction += StartAnimation;
    }

    private void StartAnimation(HealthHolder healthHolder)
    {
        gameObject.SetActive(false);
    }
}
