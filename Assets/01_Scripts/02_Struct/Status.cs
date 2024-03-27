namespace EntityManage
{
    [System.Serializable]
    public struct Status
    {
        public int hp;

        private int hpMax;
        public int HpMax
        {
            get { return hpMax; }
            private set { }
        }

        public int attackDamage;
        public int criticalRate;
        public int defense;
        public int criticalDefense;

        public float moveSpeed;
        public float mass;

        public bool isHealDefense;
        public float healthDefMultiple;
    }
}