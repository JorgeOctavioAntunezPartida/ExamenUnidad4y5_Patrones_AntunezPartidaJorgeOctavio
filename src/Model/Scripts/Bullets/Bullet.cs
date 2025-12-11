using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int _id;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    public int Damage => _damage;
    public float Speed => _speed;
    #endregion

    #region Eventos Propios
    public static event Action<GameObject> OnBulletTurretImpacta;
    public static event Action<GameObject> OnBulletCanonImpacta;
    public static event Action<GameObject> OnBulletMachineGunImpacta;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
            return;

        if (this.gameObject.name == "BulletTurret(Clone)") OnBulletTurretImpacta?.Invoke(this.gameObject);
        if (this.gameObject.name == "BulletCanon(Clone)") OnBulletCanonImpacta?.Invoke(this.gameObject);
        if (this.gameObject.name == "BulletMachineGun(Clone)") OnBulletMachineGunImpacta?.Invoke(this.gameObject);
    }
}