using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    [SerializeField] private WeaponShopSO _shopSO;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private RectTransform _rect;

    public WeaponShopSO ShopSO 
    {
        get => _shopSO;
        set => _shopSO = value;
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
    }
    public void Open()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_rect.DOScaleX(700f, 0.5f));
        seq.Append(_rect.DOScaleY(500f, 0.5f));

        seq.Play();
    }

    public void Close()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_rect.DOScaleX(0f, 0.5f));
        seq.Append(_rect.DOScaleY(0f, 0.5f));

        seq.Play();
    }
}
