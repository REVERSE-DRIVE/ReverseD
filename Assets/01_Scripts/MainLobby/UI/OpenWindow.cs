using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenWindow : MonoSingleton<OpenWindow>
{
    [Header("UI")]
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    public WeaponShopSO _shopSO;

    [Header("Button")]
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    private RectTransform _rect;

    public Button YesBtn
    {
        get => _yesBtn;
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
        _yesBtn.onClick.AddListener(Close);
        _noBtn.onClick.AddListener(Close);
    }
    public void Open()
    {
        _weaponImage.sprite = _shopSO.icon;
        _descriptionText.text = _shopSO.name;
        Sequence seq = DOTween.Sequence();

        seq.Append(_rect.DOScaleX(1f, 0.5f));
        seq.Append(_rect.DOScaleY(1f, 0.5f));

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
