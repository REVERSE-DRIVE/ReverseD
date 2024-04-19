using UnityEngine;

public class SplitProjectile : Projectile
{
    [Header("Split Setting")] [SerializeField]
    private int _splitAmount = 2;

    [SerializeField] private Projectile _splitedProjectile;

    protected override void DestroyEvent()
    {
        
    }
}