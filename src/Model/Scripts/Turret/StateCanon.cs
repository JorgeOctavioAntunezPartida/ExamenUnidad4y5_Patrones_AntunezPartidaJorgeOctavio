using System;
using UnityEngine;
using System.Collections;

public class StateCanon : IState
{
    private WeaponMode _weaponMode;
    public StateCanon(WeaponMode weaponMode) => _weaponMode = weaponMode;

    [Header("Cooldowns")]
    [SerializeField] private float canonCooldown = 1.25f;
    private bool canShoot = true;

    public static event Action<Vector3> OnShootCanon;

    public void Shoot(Vector3 position)
    {
        if (!canShoot) return;
        OnShootCanon?.Invoke(position);
        _weaponMode.StartStateCoroutine(CanShot());
    }

    public IEnumerator CanShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(canonCooldown);
        canShoot = true;
    }
}