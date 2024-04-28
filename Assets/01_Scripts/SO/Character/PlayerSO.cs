using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    public string playerDescription;
    [Space(20)]
    public Sprite playerSprite;
    public int playerHealth;
    public int moveSpeed;
    public int arrange;

    public void SetCharacter()
    {
        // 무기 = 캐릭터 일때 당시의 코드임
        // 스킬이랑 연결 하셈
        
        // Transform player = PlayerManager.Instance.transform;
        // switch (weaponType)
        // {
        //     case WeaponType.lazor:
        //         PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Fileless>();
        //         break;
        //     case WeaponType.shield:
        //         PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Ransomware>();
        //         break;
        //     case WeaponType.bow:
        //         PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Adware>();
        //         break;
        //     case WeaponType.sword:
        //         PlayerManager.Instance.PlayerAttack = player.GetChild(0).GetComponent<Malware>();
        //         break;
        // }
    }
}
