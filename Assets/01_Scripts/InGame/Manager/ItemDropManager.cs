using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using ItemManage;
using UnityEngine;

public class ItemDropManager : MonoSingleton<ItemDropManager>
{
    
    [SerializeField] private GameObject DropItemPrefab;
    /**
     * <summary>
     * 아이템 데이터 정보가 모두 저장되어있음
     * </summary>
     */
    [SerializeField] private Items _items; // ItemBase
    // 아이템 드롭 타입에 따라 다른 베이스에서 랜덤 픽하여 아이템을 드랍하기
    [SerializeField] public ItemDropBase[] _itemDropBases;
    
    /**
     * <summary>
     * itemDropType에 따라 랜덤픽되어 아이템을 지급
     * EX) 데이터 가드를 잡음 -> Type : Normal : 데이터 조각 / 데이터 파편 / 주소 데이터  중 1 드랍
     * </summary>
     */
    public GameObject DropItem(ItemDropType itemDropType, Vector2 position)
    {
        //GameObject dropItem = PoolManager.Get(DropItemPrefab, position, Quaternion.identity);
        // 아래 오버로딩된 DropItem을 이용해서 랜덤 드랍
        GameObject dropItem = DropItem(_itemDropBases[(int)itemDropType].GetRandomItem(), position);
        return dropItem;
    }
    
    /**
     * <summary>
     * 실실적으로 아이템을 드랍 시키는 함수
     * </summary>
     */
    public GameObject DropItem(Item item, Vector2 position)
    {
        GameObject dropItem = PoolManager.Get(DropItemPrefab, position, Quaternion.identity);
        dropItem.GetComponent<DropItem>().SetItem(item, _items.FindItem(item.id));

        return dropItem;
    }
    
    
}
