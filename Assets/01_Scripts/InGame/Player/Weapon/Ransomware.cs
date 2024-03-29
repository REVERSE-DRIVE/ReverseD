using System.Collections;
using UnityEngine;

public class Ransomware : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        _attackColliders[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        _attackColliders[3].gameObject.SetActive(false);
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}