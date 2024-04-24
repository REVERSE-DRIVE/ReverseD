using System;
using Unity.VisualScripting;
using UnityEngine;

public class GravityTargetObject : MonoBehaviour
{
    [SerializeField] private bool isAffectGravity = true;
    
    private Vector2 _gravityDirection;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _rigid.AddForce(_gravityDirection, ForceMode2D.Impulse);
    }
}