using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/WeaponSO", order = 1)]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public int rank;
    public List<int> damage;
}
