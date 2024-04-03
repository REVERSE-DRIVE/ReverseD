using System.Collections;
using UnityEngine;

// 활 쏘는 애
public class Adware : PlayerAttack
{
    [SerializeField] private GameObject _arrowPrefab;
    public override IEnumerator AttackRoutine()
    {
        GameObject arrow = PoolManager.Get(_arrowPrefab, transform.position, transform.rotation, transform.GetChild(1));
        yield return new WaitForSeconds(PlayerManager.Instance.PlayerSO.attackSpeed);
        isAllowAttack = true;
    }
}