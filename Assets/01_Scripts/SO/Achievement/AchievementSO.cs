using UnityEngine;

namespace AchievementManage
{
    [CreateAssetMenu(menuName = "SO/Achievement")]
    [System.Serializable]
    public class AchievementSO : ScriptableObject
    {
        public int id;
        public AchievementType achievementType;

        public int progress;
        public int maxProgress;

    }
}