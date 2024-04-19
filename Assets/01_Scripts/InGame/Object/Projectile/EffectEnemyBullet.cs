using EffectManage;
using UnityEngine;

public class EffectEnemyBullet : Projectile
{
    [SerializeField] private EffectPropertyInEnemyBullet[] effectPropertys;
    protected override void DamageToPlayer(Player player)
    {
        base.DamageToPlayer(player);
        for (int i = 0; i < effectPropertys.Length; i++)
        {
            GameManager.Instance._PlayerEffectManager.GetEffect(
                effectPropertys[i].effectType,
                effectPropertys[i]._effectLevel,
                effectPropertys[i]._duration
                );
        }
        
    }

    protected override void DestroyEvent()
    {
        
    }
}