using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region Pools
    public Queue<GameObject> bulletTurretPool = new Queue<GameObject>();
    [SerializeField] private GameObject bulletTurret;
    [SerializeField] private int turretPoolSize;

    public Queue<GameObject> bulletCanonPool = new Queue<GameObject>();
    [SerializeField] private GameObject bulletCanon;
    [SerializeField] private int canonPoolSize;

    public Queue<GameObject> bulletMachineGunPool = new Queue<GameObject>();
    [SerializeField] private GameObject bulletMachineGun;
    [SerializeField] private int machineGunPoolSize;
    #endregion

    void Start()
    {
        CreatePool(bulletTurretPool, bulletTurret, turretPoolSize);
        CreatePool(bulletCanonPool, bulletCanon, canonPoolSize);
        CreatePool(bulletMachineGunPool, bulletMachineGun, machineGunPoolSize);
    }

    void CreatePool(Queue<GameObject> pool, GameObject currentBullet, int queueCount)
    {
        for (int i = 0; i < queueCount; i++)
        {
            GameObject bullet = Instantiate(currentBullet);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            bullet.transform.parent = transform;
            // Temporary position off-screen

            if (bulletScript != null)
            {
                bulletScript.Id = i;
            }

            pool.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    public GameObject ObtainBullet(Queue<GameObject> pool)
    {
        if (pool.Count > 0)
        {
            GameObject bullet = pool.Dequeue();

            bullet.SetActive(true);
            return bullet;
        }
        return null;
    }

    public void ReturnBullet(GameObject bullet, Queue<GameObject> pool)
    {
        bullet.transform.position = transform.position; // Temporary position off-screen
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
    private void FireBullet(Vector3 pos, Queue<GameObject> pool)
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("No hay balas en la piscina!");
            SuccessfulShot(false);
            return;
        }

        GameObject bullet = ObtainBullet(pool);
        bullet.transform.position = pos;

        Rigidbody2D rb_;
        rb_ = bullet.GetComponent<Rigidbody2D>();
        rb_.linearVelocity = transform.right * bullet.GetComponent<Bullet>().Speed;
        SuccessfulShot(true);
    }


    #region Eventos Propios
    public static event Action<bool> OnSuccessfulShot;

    public static void SuccessfulShot(bool successfulShot)
    {
        OnSuccessfulShot?.Invoke(successfulShot);
    }
    #endregion

    #region Eventos Susbcritos
    private void OnEnable()
    {
        StateTurret.OnShootTurret += FireBulletTurret;
        StateCanon.OnShootCanon += FireBulletCanon;
        StateMachineGun.OnShootMachineGun += FireBulletMachineGun;

        Bullet.OnBulletTurretImpacta += ReturnBulletTurret;
        Bullet.OnBulletCanonImpacta += ReturnBulletCanon;
        Bullet.OnBulletMachineGunImpacta += ReturnBulletMachineGun;
    }

    private void OnDisable()
    {
        StateTurret.OnShootTurret -= FireBulletTurret;
        StateCanon.OnShootCanon -= FireBulletCanon;
        StateMachineGun.OnShootMachineGun -= FireBulletMachineGun;

        Bullet.OnBulletTurretImpacta -= ReturnBulletTurret;
        Bullet.OnBulletCanonImpacta -= ReturnBulletCanon;
        Bullet.OnBulletMachineGunImpacta -= ReturnBulletMachineGun;
    }

    void FireBulletTurret(Vector3 pos)
    {
        FireBullet(pos, bulletTurretPool);
    }

    void FireBulletCanon(Vector3 pos)
    {
        FireBullet(pos, bulletCanonPool);
    }

    void FireBulletMachineGun(Vector3 pos)
    {
        FireBullet(pos, bulletMachineGunPool);
    }

    void ReturnBulletTurret(GameObject bullet)
    {
        ReturnBullet(bullet, bulletTurretPool);
    }
    void ReturnBulletCanon(GameObject bullet)
    {
        ReturnBullet(bullet, bulletCanonPool);
    }
    void ReturnBulletMachineGun(GameObject bullet)
    {
        ReturnBullet(bullet, bulletMachineGunPool);
    }
    #endregion
}