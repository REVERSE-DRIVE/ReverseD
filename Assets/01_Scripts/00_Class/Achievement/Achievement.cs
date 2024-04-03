namespace AchievementManage
{
    public class Achievement
    {
        public int id;
        public int progress;
        public int maxProgress = 1;

        public bool IsClear
        {
            get { return progress >= maxProgress; }
            private set { }
        }
    }
}