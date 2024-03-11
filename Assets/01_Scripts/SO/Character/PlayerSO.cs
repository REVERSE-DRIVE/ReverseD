using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public Sprite playerSprite;
    public int playerHealth;
    public int attackRange;
    public int speed;
    public int arrange;
    public int attackSpeed;
    public int knockback;
    public WeaponType weaponType;
}
