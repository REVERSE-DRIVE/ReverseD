using EntityManage;
using TMPro;
using UnityEngine;

namespace EnemyManage
{
    
    
    [CreateAssetMenu(menuName = "SO/BossData")]
    [System.Serializable]
    public class BossData : ScriptableObject
    {
        [Header("Boss CutScene Setting")]
        public TMP_FontAsset font;
        public string bossName;
        [ColorUsage(true)]
        public Color bossNameColor;
        public Sprite bossImage;
        public float spriteSizeOffset = 1;
        [Header("Boss Setting")]
        public Status bossStatus;
        public Boss _bossPrefab;

        public Boss GetBoss
        {
            get {
            
                _bossPrefab.defaultStatus = this.bossStatus;
                return _bossPrefab;
            }
        }
    }
}