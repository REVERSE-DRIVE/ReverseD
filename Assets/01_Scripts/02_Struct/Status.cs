namespace entityManage
{
    [System.Serializable]
    public struct Status
    {
        private int hp;

        public int Hp
        {
            get { return hp; }
            private set { }
        }
        private int hpMax;
        public int criticalRate;
        public int defense;
        public int criticalDefense;
        
    }
}