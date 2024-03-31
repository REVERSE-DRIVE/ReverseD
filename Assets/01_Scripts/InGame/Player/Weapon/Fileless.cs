using System.Collections;
using EnemyManage;
using UnityEngine;

public class Fileless : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        RaycastHit2D[] coll = Physics2D.RaycastAll(transform.position, dir, 1, _whatIsEnemy);
        foreach (RaycastHit2D c in coll)
        {
            c.collider.GetComponent<Enemy>().TakeDamage(PlayerManager.Instance.PlayerSO.attackDamage);
        }
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}