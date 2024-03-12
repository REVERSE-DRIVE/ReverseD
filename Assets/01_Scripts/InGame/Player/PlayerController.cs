using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private Collider2D[] attackColliders;
    [SerializeField] private GameObject _arrowPrefab;
    
    private SpriteRenderer _spriteRenderer;
    private PlayerManager _playerManager;
    private GameObject _playerAttackCollider;
    private Player _player;
    private Vector3 dir;
    private List<GameObject> arrows = new List<GameObject>();

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _player = GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        attackColliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        _playerAttackCollider = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        ArrowSpawn(30);
    }

    void Update()
    {
        Move();
        Rotate();
        if (Input.GetMouseButtonDown(1))
        {
            Attack();
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
        _playerAttackCollider.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Attack()
    {
        switch (_playerManager.WeaponType)
        {
            case WeaponType.sword:
                foreach (var col in attackColliders)
                {
                    col.enabled = false;
                }
                attackColliders[0].enabled = true;
                break;
            case WeaponType.bow:
                foreach (var col in attackColliders)
                {
                    col.enabled = false;
                }
                transform.GetChild(1).rotation = _playerAttackCollider.transform.rotation;
                ArrowShot();
                break;  
            case WeaponType.lazor:
                foreach (var col in attackColliders)
                {
                    col.enabled = false;
                }
                attackColliders[1].enabled = true;
                break;
            case WeaponType.shield:
                foreach (var col in attackColliders)
                {
                    col.enabled = false;
                }
                attackColliders[2].enabled = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void ArrowSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject arrow = Instantiate(_arrowPrefab, transform.GetChild(1));
            arrows.Add(arrow);
            arrow.SetActive(false);
        }
    }
    
    private void ArrowShot()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                arrows[i].transform.position = transform.position;
                arrows[i].transform.rotation = transform.rotation;
                arrows[i].SetActive(true);
                break;
            }
        }
    }
    
    
    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _playerManager.PlayerSprite;
    }
}
