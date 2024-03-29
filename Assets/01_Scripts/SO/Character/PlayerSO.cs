using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public Sprite playerSprite;
    public int playerHealth;
    public int attackDamage;
    public int attackRange;
    public int moveSpeed;
    public int arrange;
    public int attackSpeed;
    public int knockback;
    public WeaponType weaponType;

    public void SetCharacter()
    {
        Transform player = PlayerManager.Instance.transform;
        switch (weaponType)
        {
            case WeaponType.lazor:
                PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Fileless>();
                break;
            case WeaponType.shield:
                PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Ransomware>();
                break;
            case WeaponType.bow:
                PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Adware>();
                break;
            case WeaponType.sword:
                PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Malware>();
                break;
        }
    }
}
