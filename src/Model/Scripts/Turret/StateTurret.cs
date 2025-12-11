using System;
using UnityEngine;
using System.Collections;

public class StateTurret : IState
{
    private WeaponMode _weaponMode;
    public StateTurret(WeaponMode weaponMode) => _weaponMode = weaponMode;

    [Header("Cooldowns")]
    [SerializeField] private float turretCooldown = 0.5f;
    private bool canShoot = true;

    public static event Action<Vector3> OnShootTurret;

    public void Shoot(Vector3 position)
    {
        if (!canShoot) return;
        OnShootTurret?.Invoke(position);
        _weaponMode.StartStateCoroutine(CanShot());
    }

    public IEnumerator CanShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(turretCooldown);
        canShoot = true;
    }
}