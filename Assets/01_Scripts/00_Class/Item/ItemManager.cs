using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            _items.itemDataList[3]
        };
        SetItemData(ItemCombination(itemDatas));
        
        Debug.Log("Item Name: " + _itemName);
        Debug.Log("Description: " + _description);
        Debug.Log("Icon: " + _icon.name);
    }

    public ItemData ItemCombination(List<ItemData> itemDatas)
    {
        List<ItemData> datas = itemDatas.Where(item => item.itemType == ItemType.data).ToList();
        List<ItemData> dataPackList = itemDatas.Where(item => item.itemType == ItemType.datapack).ToList();
        int dataCount = datas.Count;
        int dataPackCount = dataPackList.Count;

        if (dataCount + dataPackCount <= 1 || dataPackCount == 0 || dataCount == 0) return null;

        #region ItemCombination
        switch (dataCount)
        {
            case 1 when dataPackCount == 1:
            {
                return GetItemData(0, false);
            }
            case 2 when dataPackCount == 1:
            {
                return GetItemData(1, false);
            }
            case 1 when dataPackCount == 2:
            {
                return GetItemData(2, false);
            }
            case 2 when dataPackCount == 2:
            {
                return GetItemData(3, false);
            }
            default:
                return null;
        }
        #endregion
    }
}
