using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    
    
    private SpriteRenderer _spriteRenderer;
    private PlayerManager _playerManager;
    private PlayerAttack _playerAttack;
    
    private Player _player;
    private Vector3 dir;
    

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _player = GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        _playerAttack.ArrowSpawn(30);
    }

    void Update()
    {
        Move();
        Rotate();
        if (Input.GetMouseButtonDown(1))
        {
            _playerAttack.Attack(PlayerManager.Instance.WeaponType);
        }
    }


    private void Move()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(direction * (_player.Status.moveSpeed * Time.deltaTime));
    }
    
    private void Rotate()
    {
        dir = _joystick.Direction;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _playerAttack.PlayerAttackCollider.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _playerManager.PlayerSprite;
    }
}
