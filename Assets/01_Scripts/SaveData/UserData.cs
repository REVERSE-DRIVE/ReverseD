namespace SaveDatas
{
    public class UserData
    {
        public bool isFirstPlay = true;
        public string userName = "Unknown";

        public AchievementSaveData achievementData;

        public void Reset()
        {
            isFirstPlay = true;
            userName = "Unknown";
            
            
        }
    }
}