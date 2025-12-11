using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private WeaponMode weapon;
    [SerializeField] public Transform shotPoint;
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Awake()
    {
        InitializeInputSystem();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Player
    #region Attributes
    [SerializeField] private InputActionAsset InputAction;
    private InputAction moveAction;
    public Vector2 moveInput;

    public InputAction shotAction;

    public InputAction selectTurretAction;
    public InputAction selectCanonAction;
    public InputAction selectMachineGunAction;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator anim;
    #endregion

    void InitializeInputSystem()
    {
        moveAction = InputAction.FindActionMap("Player").FindAction("Move");
        shotAction = InputAction.FindActionMap("Player").FindAction("Jump");

        selectTurretAction = InputAction.FindActionMap("Player").FindAction("Turret");
        selectCanonAction = InputAction.FindActionMap("Player").FindAction("Canon");
        selectMachineGunAction = InputAction.FindActionMap("Player").FindAction("MachineGun");
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (shotAction.IsPressed())
        {
            weapon.CurrentState.Shoot(shotPoint.position);
        }

        if (selectTurretAction.WasPressedThisFrame())
        {
            weapon.SetState(weapon.Turret);
        }
        if (selectCanonAction.WasPressedThisFrame())
        {
            weapon.SetState(weapon.Canon);
        }
        if (selectMachineGunAction.WasPressedThisFrame())
        {
            weapon.SetState(weapon.MachineGun);
        }
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    #endregion

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}