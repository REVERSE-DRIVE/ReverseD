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
                return _items.itemDataList.Find(item => item.id == id);
            else
                return _itemCombinations.itemDataList.Find(item => item.id == id);
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
            SetItemData(itemDatas.First());

            Debug.Log("Item Name: " + _itemName);
            Debug.Log("Description: " + _description);
            Debug.Log("Icon: " + _icon.name);
        }

        public ItemData ItemCombination(ItemData item1, ItemData item2)
        {
            item1.SetType();
            item2.SetType();

            // 조합법
            Dictionary<(DataPackType, DataChipType), int> combinationMap =
                new Dictionary<(DataPackType, DataChipType), int>
                {
                    { (DataPackType.MalWare, DataChipType.Information), 0 },
                    { (DataPackType.MalWare, DataChipType.Dioraijation), 1 },
                    { (DataPackType.AdWare, DataChipType.PopUp), 2 },
                    { (DataPackType.AdWare, DataChipType.FrameDrop), 3 }
                };

            // 반환
            if (combinationMap.TryGetValue((item1.dataPackType, item2.dataChipType), out int combinationId))
            {
                return _itemCombinations.itemDataList.Find(item => item.id == combinationId);
            }

            // 없을 때
            return null;
        }

    }
}
