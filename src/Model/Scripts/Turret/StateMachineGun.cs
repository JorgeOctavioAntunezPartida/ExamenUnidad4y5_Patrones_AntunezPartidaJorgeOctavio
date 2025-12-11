using System;
using UnityEngine;
using System.Collections;

public class StateMachineGun : IState
{
    private WeaponMode _weaponMode;
    public StateMachineGun(WeaponMode weaponMode) => _weaponMode = weaponMode;

    [Header("Cooldowns")]
    [SerializeField] private float canonCooldown = 0.2f;
    private bool canShoot = true;

    public static event Action<Vector3> OnShootMachineGun;

    public void Shoot(Vector3 position)
    {
        if (!canShoot) return;
        OnShootMachineGun?.Invoke(position);
        _weaponMode.StartStateCoroutine(CanShot());
    }

    public IEnumerator CanShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(canonCooldown);
        canShoot = true;
    }
}