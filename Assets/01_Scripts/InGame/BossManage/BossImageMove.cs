using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossImageMove : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Ease ease;
    private void OnEnable()
    {
        transform.DOLocalMove(target.localPosition, 1f).SetEase(ease);
    }
    
}
