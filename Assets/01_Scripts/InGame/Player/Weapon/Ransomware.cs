using System.Collections;
using EnemyManage;
using UnityEngine;

public class Ransomware : PlayerAttack
{
    public override IEnumerator AttackRoutine()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(
            new Vector2(_playerTransform.x + dir.x, _playerTransform.y + dir.y), 1, _whatIsEnemy);
        foreach (Collider2D c in coll)
        {
            c.GetComponent<Enemy>().TakeDamage(PlayerManager.Instance.PlayerSO.attackDamage);
        }
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}