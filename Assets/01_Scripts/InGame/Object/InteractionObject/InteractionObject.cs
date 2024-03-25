using System;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    public string objectName;
    public string objectDescription;

    [Header("Interaction Setting Value")] [SerializeField]
    private Material _selectMaterial;

    private Material _defaultMaterial;
    
    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public virtual void InteractionDetectEvent()
    {
        _spriteRenderer.material = _selectMaterial;
    }
    public virtual void Interact()
    {
        // override로 구현
        
    }
    public virtual void InteractionUnDetectEvent()
    {
        _spriteRenderer.material = _defaultMaterial;
    }

    public void ShowName()
    {
        
    }
}
