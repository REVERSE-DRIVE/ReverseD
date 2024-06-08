using AttackManage;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponShopSO")]
public class WeaponShopSO : ScriptableObject
{
    public WeaponSO weaponSO;
    public Sprite icon;
    public int price;
}