using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Image slot;
    public void UpgradeWeapon()
    {
        WeaponSlot wpSlot = slot.GetComponentInChildren<WeaponSlot>();
        if (wpSlot == null)
        {
            return;
        }
        
        switch (wpSlot.WeaponSO.rank)
        {
            case 1:
                UpgradeRank(80f, wpSlot, 2);
                break;
            case 2:
                UpgradeRank(60f, wpSlot, 3);
                break;
            case 3:
                UpgradeRank(40f, wpSlot, 4, 41f);
                break;
            case 4:
                UpgradeRank(20f, wpSlot, 5, 25f);
                break;
            case 5:
                UpgradeRank(10f, wpSlot, 6, 20f);
                break;
        }
        Debug.Log(wpSlot.WeaponSO.rank);
    }

    private void UpgradeRank(float per, WeaponSlot wpSlot, int rank, float breakPer = 0f)
    {
        float percent = Random.Range(0f, 100f);
        if (percent <= per)
        {
            wpSlot.WeaponSO.rank = rank;
        }
        if (breakPer == 0f)
        {
            return;
        }
        if (percent > per && percent <= breakPer)
        {
            wpSlot.WeaponSO.rank = 1;
        }
    }
}
