using System.Collections;
using UnityEngine;

public class Trojan : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        _attackColliders[1].enabled = true;
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        _attackColliders[1].enabled = false;
        isAllowAttack = true;
    }
}