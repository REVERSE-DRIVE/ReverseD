using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banner : MonoBehaviour
{
    [SerializeField] private bool isUp;
    [SerializeField] private RectTransform targetUp;
    [SerializeField] private RectTransform targetDown;
    [SerializeField] private Ease ease;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (isUp)
        {
            rectTransform.localPosition = new Vector3(-2200f, 927f);
            rectTransform.DOLocalMove(targetUp.localPosition, 1f).SetEase(ease);
        }
        else
        {
            rectTransform.localPosition = new Vector3(2173f, -933f);
            rectTransform.DOLocalMove(targetDown.localPosition, 1f).SetEase(ease);
        }
    }
}
