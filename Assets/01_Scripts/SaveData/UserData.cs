namespace SaveDatas
{
    public class UserData
    {
        public bool isFirstPlay = false;
        public string userName = "Unknown";

        public AchievementSaveData achievementData;

        public void Reset()
        {
            isFirstPlay = false;
            userName = "Unknown";
            
            
        }
    }
}