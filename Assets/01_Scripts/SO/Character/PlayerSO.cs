using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public Sprite playerSprite;
    public int playerHealth;
    public int attackDamage;
    public int attackRange;
    public int moveSpeed;
    public int arrange;
    public int attackSpeed;
    public int knockback;
    public WeaponType weaponType;
}
