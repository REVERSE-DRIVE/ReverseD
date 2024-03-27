using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemManage
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private Items _items;
        [SerializeField] private Items _itemCombinations;
        private string _itemName;
        private string _description;
        private Sprite _icon;

        public ItemData GetItemData(int id, bool isItem)
        {
            if (isItem)
                return _items.FindItem(id);
            else
                return _itemCombinations.FindItem(id);
        }

        public void SetItemData(ItemData itemData)
        {
            _itemName = itemData.itemName;
            _description = itemData.description;
            _icon = itemData.icon;
        }

        private void Start()
        {
            List<ItemData> itemDatas = new List<ItemData>
            {
                _items.itemDataList[0],
                _items.itemDataList[1],
                _items.itemDataList[2],
            };
            SetItemData(ItemCombination(itemDatas.ToArray()));

            Debug.Log("Item Name: " + _itemName);
            // Debug.Log("Description: " + _description);
            // Debug.Log("Icon: " + _icon.name);
        }

        /**
         * <summary>
         * 프로토콜 타입에 따른 아이템 조합
         * </summary>
         */
        public ItemData ItemCombination(ItemData[] items)
        {
            return _itemCombinations.IsAcceptCombination(items, items[0].protocolType) 
                ? _itemCombinations.FindItem(items[0].protocolType) : null;
        }
    }
}
