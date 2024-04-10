using EntityManage;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct BossData
{
    public TMP_FontAsset font;
    public string bossName;
    [ColorUsage(true)]
    public Color bossNameColor;
    public Sprite bossImage;
    public Status bossStatus;
}