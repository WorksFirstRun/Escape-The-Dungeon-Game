using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}
    private InputMap inputMap;
    public event EventHandler onDashAction;
    public event EventHandler onAttackAction;
    public event EventHandler onInteractAction;
    public event EventHandler onEscapeAction;
    
    private void Awake()
    {
        inputMap = new InputMap();
        inputMap.Enable();
        Instance = this;
        
        
        inputMap.Player.Dash.performed += OnDashPerformed;
        inputMap.Player.Attack.performed += onAttackPerformed;
        inputMap.Player.Interact.performed += Interact;
        inputMap.Player.Esc.performed += Escape;
    }

    private void Escape(InputAction.CallbackContext obj)
    {
        onEscapeAction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        onInteractAction?.Invoke(this,EventArgs.Empty);
    }

    private void onAttackPerformed(InputAction.CallbackContext obj)
    {
       onAttackAction?.Invoke(this,EventArgs.Empty);
    }

    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        onDashAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        onDashAction = null;
        onAttackAction = null;
        onEscapeAction = null;
        onInteractAction = null;
    }


    public Vector2 GetInputDirections()
    {
        Vector2 directions = inputMap.Player.Move.ReadValue<Vector2>();
        return directions;
    }
    
    
    
    
}
