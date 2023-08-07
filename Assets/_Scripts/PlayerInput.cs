using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnShootPressed;
    public event Action OnShootReleased;

    public event Action OnDashStarted;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerControls.Player.Shoot.started += OnShootStarted;
        playerControls.Player.Shoot.canceled += OnShootStopped;
        playerControls.Player.Dash.started += Dash_started;
    }

    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashStarted?.Invoke();
    }

    private void OnDisable()
    {
        playerControls.Player.Shoot.started -= OnShootStarted;
        playerControls.Player.Shoot.canceled -= OnShootStopped;
    }

    private void OnShootStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootPressed?.Invoke();
    }

    private void OnShootStopped(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootReleased?.Invoke();
    }

    public Vector2 GetMovementVector()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
}
