using System.Collections.Generic;
using UnityEngine;

namespace ItemManage
{
    public class InventoryManager : MonoBehaviour
    {
        public Items itemData;
        public List<Item> inventory;
        
        public void AddItem(int id, int amount)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].id == id)
                {
                    inventory[i].amount += amount;
                    return;
                }
            }
            inventory.Add(new Item(id, amount));
        }
        
        public Sprite FindItemSprite(int id)
        {
            return itemData.FindItem(id).icon;
        }

    }
}