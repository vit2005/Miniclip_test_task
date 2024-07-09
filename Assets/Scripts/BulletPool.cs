using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialPoolSize = 10;
    public int maxPoolSize = 20;

    private IObjectPool<GameObject> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, false, initialPoolSize, maxPoolSize);
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        return bullet;
    }

    private void OnGetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    private void OnReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public Bullet GetBullet()
    {
        return bulletPool.Get().GetComponent<Bullet>();
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bulletPool.Release(bullet);
    }
}
