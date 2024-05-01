using UnityEngine;

public class PlayerSOMover : MonoSingleton<PlayerSOMover>
{
    [field:SerializeField] public PlayerSO PlayerSO { get; set; }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    public void SetSO(PlayerSO playerSO)
    {
        PlayerSO = playerSO;
    }
}
