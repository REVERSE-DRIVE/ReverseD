using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    private SpriteRenderer _spriteRenderer;
    private PlayerAttack _playerAttack;
    private Rigidbody2D _rigid;
    private Player _player;
    private Vector3 dir;
    
    public VariableJoystick Joystick 
    { 
        get => _joystick;
        set => _joystick = value;
        
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAttack = GetComponent<PlayerAttack>();
        _rigid = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _playerAttack.ArrowSpawn(30);
        PlayerManager.Instance.UpdateStat();
    }

    void Update()
    {
        Move();
        Rotate();
    }


    private void Move()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        _rigid.velocity = direction * (PlayerManager.Instance.Speed * TimeManager.TimeScale);
    }
    
    private void Rotate()
    {
        dir = _joystick.Direction;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _playerAttack.PlayerAttackCollider.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = PlayerManager.Instance.PlayerSprite;
    }
}