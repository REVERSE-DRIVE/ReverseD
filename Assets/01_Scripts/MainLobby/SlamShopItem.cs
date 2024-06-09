using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SlamShopItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<WeaponShopSO> _weaponSOs;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private bool _isPurchased = false;
    private WeaponShopSO _nowWeapon;

    private void Awake()
    {
        OpenWindow.Instance.YesBtn.onClick.AddListener(Purchase);
        OpenWindow.Instance.Close();
        SetShopWeaponData(Random.Range(0, _weaponSOs.Count));
    }

    public void SetShopWeaponData(int index)
    {
        _isPurchased = false;
        if (_weaponSOs == null) return;
        _nowWeapon = _weaponSOs[index];
        _iconImage.sprite = _nowWeapon.icon;
        _nameText.text = _nowWeapon.weaponSO.weaponName;
        _priceText.text = $"{_nowWeapon.price} 코인";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isPurchased = true;
        SavePurchaseData();
    }

    private void SavePurchaseData()
    {
        if (_isPurchased)
        {
            OpenWindow.Instance._shopSO = _nowWeapon;
            OpenWindow.Instance.Open();
        }
    }

    private void Purchase()
    {
        // 무기 넘겨주기
    }
}
