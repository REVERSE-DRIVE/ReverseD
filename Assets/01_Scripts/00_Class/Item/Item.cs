
namespace ItemManage
{
    
    [System.Serializable]
    public class Item
    {
        public int id;
        public int amount;

        public Item(int id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
}