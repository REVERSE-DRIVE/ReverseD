
namespace EntityManage
{
    
    public interface IDamageable
    {
        
        public void TakeDamage(int damage);

        public void TakeCriticalDamage(int damage);

        public void Die();
    }
}
