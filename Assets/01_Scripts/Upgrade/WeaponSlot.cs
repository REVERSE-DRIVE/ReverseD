using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    public WeaponSO WeaponSO => weaponSO;
}