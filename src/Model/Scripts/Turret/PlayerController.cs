using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Attributes
    [Header("Vida")]
    [SerializeField] private float health;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private GameManager _gm;
    #endregion

    private void Start()
    {
        _gm = GameManager.Instance;
    }

    void FixedUpdate()
    {
        _gm.rb.MovePosition(_gm.rb.position + _gm.moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player recibió daño: " + amount);
    }
}