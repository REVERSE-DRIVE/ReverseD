using System.Collections;
using UnityEngine;

public class Ransomware : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        _attackColliders[2].enabled = true;
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        _attackColliders[2].enabled = false;
        isAllowAttack = true;
    }
}