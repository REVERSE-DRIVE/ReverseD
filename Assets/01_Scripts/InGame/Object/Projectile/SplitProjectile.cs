using Calculator;
using UnityEngine;

public class SplitProjectile : Projectile
{ 
    [Header("Split Setting")]
    [SerializeField]
    [Range(2, 16)]
    private short _splitAmount = 2;

    [SerializeField] private Projectile _splitedProjectile;

    protected override void DestroyEvent()
    {
        Vector2[] directions = VectorCalculator.DirectionsFromCenter(_splitAmount);
        for (short i = 0; i < _splitAmount; i++)
        {
            Projectile projectile = PoolManager.Get(_splitedProjectile, transform.position, Quaternion.identity);
            projectile.Fire(directions[i]);
        }
    }
}