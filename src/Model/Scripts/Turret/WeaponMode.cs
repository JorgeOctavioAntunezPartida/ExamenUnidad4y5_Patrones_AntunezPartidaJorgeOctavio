using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public interface IState
{
    public void Shoot(Vector3 position);
    IEnumerator CanShot();
}

public class WeaponMode : MonoBehaviour
{
    public IState Turret { get; set; }
    public IState Canon { get; set; }
    public IState MachineGun { get; set; }
    public IState CurrentState { get; set; }

    public void Awake()
    {
        Turret = new StateTurret(this);
        Canon = new StateCanon(this);
        MachineGun = new StateMachineGun(this);
        SetState(Turret);
    }

    public void SetState(IState newState)
    {
        CurrentState = newState;
    }

    public void StartStateCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void Shoot(Vector3 position) => CurrentState.Shoot(position);
    public IEnumerator CanShot() => CurrentState.CanShot();
}