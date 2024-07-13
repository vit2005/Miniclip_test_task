using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    public float speed = 10f;

    private int _targetLayers;
    private int _attackPower;

    public void Init(LayerMask layers, int attackPower)
    {
        _targetLayers = layers;
        _attackPower = attackPower;
        trail.Clear();
    }

    public void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!LayersHelper.IsLayerInLayerMask(collision.gameObject.layer, _targetLayers)) return;
        var healthHolder = collision.gameObject.GetComponent<HealthHolder>();
        healthHolder.Damage(_attackPower);
        gameObject.SetActive(false);
    }
}
