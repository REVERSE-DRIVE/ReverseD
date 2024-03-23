using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using ItemManage;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField] private GameObject DropItemPrefab;
    [SerializeField] private Items _items;

    
    public GameObject DropItem(ItemDropType itemDropType, Vector2 position)
    {
        GameObject dropItem = PoolManager.Get(DropItemPrefab, position, Quaternion.identity);


        return dropItem;
    }
    
    public GameObject DropItem(Item item, Vector2 position)
    {
        GameObject dropItem = PoolManager.Get(DropItemPrefab, position, Quaternion.identity);
        dropItem.GetComponent<DropItem>().SetItem(item, _items.FindItem(item.id));

        return dropItem;
    }
    
    
}
