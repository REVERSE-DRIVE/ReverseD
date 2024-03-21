using UnityEngine;

namespace AchievementManage
{
    [CreateAssetMenu(menuName = "SO/Achievement")]
    [System.Serializable]
    public class AchievementSO : ScriptableObject
    {
        public int id;
        public string achievementName;
        public AchievementType achievementType;
        public string achievementDescription;
        
        public int maxProgress;

    }
}