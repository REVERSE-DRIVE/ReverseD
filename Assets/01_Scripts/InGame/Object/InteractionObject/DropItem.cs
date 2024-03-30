using System;
using UnityEngine;
using ItemManage;

public class DropItem : InteractionObject
{
    [SerializeField] private Item _dropItem;
    private ItemData _itemData;
    
    /**
     * <summary>
     * 드롭 아이템 오브젝트의 아이템 설정
     * </summary>
     */
    public void SetItem(Item item, ItemData itemData)
    {
        _dropItem = item;
        _itemData = itemData;
        objectName = _itemData.name;
        objectDescription = _itemData.description;
        _spriteRenderer.sprite = _itemData.icon;
    }
    
    private void OnEnable()
    {
        
        
    }

    public override void Interact()
    {

        
        Destroy();

    }

    private void Destroy()
    {
        _dropItem = null;
        _itemData = null;
        
        PoolManager.Release(gameObject);
    }


}