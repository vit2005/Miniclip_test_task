using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour
{
    public Transform target;
    public Transform spawnPoint;
    public float fireRate = 1f;
    public Layers targetLayer;
    private float nextFireTime = 0f;
    private BulletPool bulletPool;

    private void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Fire()
    {
        Bullet bullet = bulletPool.GetBullet();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(target.position - spawnPoint.position);
        bullet.gameObject.SetActive(true);
        bullet.SetTargetLayer((int)targetLayer);
        bullet.Move();

        // Return the bullet to the pool after some time (e.g., 2 seconds)
        StartCoroutine(ReturnBulletToPool(bullet.gameObject, 2f));
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletPool.ReleaseBullet(bullet);
    }
}
