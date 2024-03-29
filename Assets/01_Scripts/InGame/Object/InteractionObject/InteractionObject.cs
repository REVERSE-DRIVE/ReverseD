using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractionObject : MonoBehaviour
{
    public string objectName;
    public string objectDescription;

    [Header("Interaction Setting Value")] [SerializeField]
    private Material _selectMaterial;

    private Material _defaultMaterial;
    
    protected SpriteRenderer _spriteRenderer;

    public UnityEvent UnDetectInteractionEvent;    
    public UnityEvent InteractionEvent;
    public UnityEvent DetectInteractionEvent;
    [Header("State Info")]
    public bool canInteraction = true;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public virtual void InteractionDetectEvent()
    {
        _spriteRenderer.material = _selectMaterial;
        DetectInteractionEvent?.Invoke();
    }
    public virtual void Interact()
    {
        if (!canInteraction)
        {
            return;
        }
        Debug.Log($"<{gameObject.name}> 상호작용 활성화 됨");
        InteractionEvent?.Invoke();
        // override로 구현
        
    }
    public virtual void InteractionUnDetectEvent()
    {
        _spriteRenderer.material = _defaultMaterial;
        UnDetectInteractionEvent?.Invoke();
    }

    public void ShowName()
    {
        
    }
}
