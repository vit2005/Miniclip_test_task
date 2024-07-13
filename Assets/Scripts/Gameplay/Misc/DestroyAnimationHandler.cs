using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthHolder))]
public class DestroyAnimationHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem _ps;

    private void Start()
    {
        GetComponent<HealthHolder>().DestroyedAction += StartAnimation;
        if (_ps == null) return;

        var collision = _ps.collision;
        collision.AddPlane(GameController.Instance.Ground);
    }

    private void StartAnimation(HealthHolder healthHolder)
    {
        StartCoroutine(DelayedDeactivation());
        if (_ps != null) _ps.Play();
    }

    private IEnumerator DelayedDeactivation()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
