using System.Collections;
using UnityEngine;

public class Ransomware : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        _attackColliders[3].enabled = true;
        yield return new WaitForSeconds(attackTime);
        _attackColliders[3].enabled = false;
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}