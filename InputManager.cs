using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private Vector2 movementInput;
    private PlayerMotor motor;
    private PlayerLook look;

    public PlayerInput.OnFootActions OnFoot => onFoot;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;

            // Hubungkan fungsi jump di sini juga agar lebih aman
            onFoot.Jump.performed += ctx => motor.Jump();
        }
        
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}