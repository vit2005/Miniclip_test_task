using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour, IUpdatable
{
    public Transform _target;
    public Transform spawnPoint;
    private float _attackSpeed;
    private int _attackPower;
    private LayerMask _targetLayers;
    private float nextFireTime = 0f;
    private BulletPool _bulletPool;

    public void Init(BulletPool bulletPool, float attackSpeed, int attackPower, LayerMask layerMask)
    {
        _bulletPool = bulletPool;
        _attackSpeed = attackSpeed;
        _attackPower = attackPower;
        _targetLayers = layerMask;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void OnUpdate()
    {
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / _attackSpeed;
        }
    }

    private void Fire()
    {
        Bullet bullet = _bulletPool.GetBullet();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(_target.position - spawnPoint.position);
        bullet.gameObject.SetActive(true);
        bullet.Init(_targetLayers, _attackPower);
        bullet.Move();

        // Return the bullet to the pool after 2 seconds
        StartCoroutine(ReturnBulletToPool(bullet.gameObject, 2f));
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        _bulletPool.ReleaseBullet(bullet);
    }
}
