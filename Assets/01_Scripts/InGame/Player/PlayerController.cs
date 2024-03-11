using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    private PlayerManager _playerManager;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(direction * (_playerManager.Speed * Time.deltaTime));
    }
    
    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _playerManager.PlayerSprite;
    }
}
