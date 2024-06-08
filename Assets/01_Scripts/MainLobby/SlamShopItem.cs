using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlamShopItem : MonoBehaviour
{
    [SerializeField] private WeaponShopSO _weaponSO;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private void OnValidate()
    {
        SetShopWeaponData();
    }

    public void SetShopWeaponData()
    {
        if (_weaponSO == null) return;
        _iconImage.sprite = _weaponSO.icon;
        _nameText.text = _weaponSO.weaponSO.weaponName;
        _priceText.text = $"{_weaponSO.price} 코인";
    }
}
