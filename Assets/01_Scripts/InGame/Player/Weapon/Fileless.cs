using System.Collections;
using UnityEngine;

public class Fileless : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        _attackColliders[2].enabled = true;
        yield return new WaitForSeconds(attackTime);
        _attackColliders[2].enabled = false;
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}