using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private int _targetLayer;

    public void SetTargetLayer(int layer)
    {
        _targetLayer = layer;
    }

    public void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Handle collision (e.g., damage to the target, deactivate bullet, etc.)
        if (collision.gameObject.layer != _targetLayer) return;
        gameObject.SetActive(false);
    }
}
