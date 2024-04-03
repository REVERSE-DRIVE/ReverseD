using System;
using UnityEngine;
using ItemManage;
using TMPro;

public class DropItem : InteractionObject
{
    [SerializeField] private Item _dropItem;
    private ItemData _itemData;
    private TMP_Text _nameText;

    protected override void Awake()
    {
        base.Awake();
        _nameText = GetComponentInChildren<TMP_Text>();
    }

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
        _nameText.text = objectName;
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
        _nameText.text = "";
        PoolManager.Release(gameObject);
    }

    public override void InteractionDetectEvent()
    {
        base.InteractionDetectEvent();
        _nameText.gameObject.SetActive(true);
    }
    
    public override void InteractionUnDetectEvent()
    {
        base.InteractionUnDetectEvent();
        _nameText.gameObject.SetActive(false);
    }

}