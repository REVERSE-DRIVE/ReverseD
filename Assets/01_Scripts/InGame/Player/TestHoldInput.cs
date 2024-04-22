using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestHoldInput : MonoBehaviour
{
    [SerializeField] private bool isHold;
    
    private MainInput _mainInput;

    private void Awake()
    {
        _mainInput = new MainInput();
        
        _mainInput.Enable();
    }

    private void Start()
    {
        _mainInput.PlayerController.Hold.performed += OnHoldPerformed;
        _mainInput.PlayerController.Hold.canceled += OnHoldCanceled;
    }
    
    private void OnHoldPerformed(InputAction.CallbackContext context)
    {
        isHold = context.ReadValueAsButton();
    }
    
    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        isHold = false;
    }
}
