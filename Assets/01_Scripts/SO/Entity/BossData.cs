using EntityManage;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BossData")]
[System.Serializable]
public class BossData : ScriptableObject
{
    public TMP_FontAsset font;
    public string bossName;
    [ColorUsage(true)]
    public Color bossNameColor;
    public Sprite bossImage;
    public Status bossStatus;
}