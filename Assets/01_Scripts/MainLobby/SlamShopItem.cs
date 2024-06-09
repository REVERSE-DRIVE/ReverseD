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

    public void SetShopWeaponData(int index)
    {
        if (_weaponSOs == null) return;
        _iconImage.sprite = _weaponSOs[index].icon;
        _nameText.text = _weaponSOs[index].weaponSO.weaponName;
        _priceText.text = $"{_weaponSOs[index].price} 코인";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isPurchased) return;
        _isPurchased = true;
        
        SavePurchaseData();
    }

    private void SavePurchaseData()
    {
        if (_isPurchased)
        {
            Debug.Log("구매 완료");
            
            // 플레이어에게 무기 지급
            // 코인 차감
            // 무기 교체
            int randomIndex = Random.Range(0, _weaponSOs.Count);
            SetShopWeaponData(randomIndex);
        }
    }
}
