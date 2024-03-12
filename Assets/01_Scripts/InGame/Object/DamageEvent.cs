using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageEvent : EventObject
{
    [SerializeField] private Vector2 defaultPos;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Vector3 defaultScale = Vector3.one;
    [SerializeField] private Vector3 targetScale;
    [Space(15f)] [SerializeField] private Transform MoveTrs;
    [SerializeField] private float defaultFontSize = 7;
    [SerializeField] private TextMeshPro damageText;

    private float currentTime = 0;

    private bool isActive;
    
    private void OnEnable()
    {
        SetDefault();
    }

    public void ActiveEvent(int damage, bool isCritical)
    {
        
        damageText.text = damage.ToString();
        if (isCritical)
        {
            damageText.fontSize = 8.5f;
            damageText.color = Color.red;

        }
        else
        {
            damageText.fontSize = defaultFontSize;
            damageText.color = Color.white;

        }
        SetDefault();
        isActive = true;

    }
    
    void Update()
    {
        if (isActive)
        {
            LifeRoutine();

        }
    }

    protected override void LifeRoutine()
    {
        currentTime += Time.deltaTime;
        float t = currentTime / lifetime;
        transform.localScale = Vector3.Lerp(defaultScale, targetScale, t);
        transform.position = Vector2.Lerp(defaultPos, targetPos, t);
        if (currentTime > lifetime)
        {
            isActive = false;
        }
    }

    public void SetDefault()
    {
        currentTime = 0;
        isActive = false;
        transform.position = defaultPos;
        transform.localScale = defaultScale;
    }
}
