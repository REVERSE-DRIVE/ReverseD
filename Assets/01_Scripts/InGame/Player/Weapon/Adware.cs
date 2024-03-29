using System.Collections;
using UnityEngine;

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