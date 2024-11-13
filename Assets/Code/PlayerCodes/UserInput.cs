using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public Vector2 MoveInput { get; private set; }
    public bool JumpJustPressed {  get; private set; }
    public bool JumpBeingHeld { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool ShootInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashDownInput { get; private set; }
    public Vector2 AimInput { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }
    public bool ContinueInput { get; private set; }

    PlayerInput _playerInput;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;
    InputAction dashAction;
    InputAction dashDownAction;
    InputAction AimAction;
    InputAction menuOpenCloseAction;
    InputAction continueAction;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    void SetupInputActions()
    {
        moveAction = _playerInput.actions["Move"];
        jumpAction = _playerInput.actions["Jump"];
        dashAction = _playerInput.actions["Dash"];
        dashDownAction = _playerInput.actions["DashDown"];
        shootAction = _playerInput.actions["Shoot"];
        AimAction = _playerInput.actions["Aim"];
        menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
        continueAction = _playerInput.actions["Continue"];
    }

    void UpdateInputs()
    {
        MoveInput = moveAction.ReadValue<Vector2>();
        JumpJustPressed = jumpAction.WasPressedThisFrame();
        JumpBeingHeld = jumpAction.IsPressed();
        JumpReleased = jumpAction.WasReleasedThisFrame();
        ShootInput = shootAction.WasPressedThisFrame();
        DashInput = dashAction.WasPressedThisFrame();
        DashDownInput = dashDownAction.WasPressedThisFrame();
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
        ContinueInput = continueAction.WasPressedThisFrame();
        AimInput = AimAction.ReadValue<Vector2>();
    }

}
