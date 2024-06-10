using ItemManage;
using UnityEngine;

public class NormalFieldObject : FieldObject
{
    [SerializeField] private Item _dropItem;
    
    protected override void DestroyEvent()
    {
        ItemDropManager.Instance.DropItem(_dropItem, transform.position);

    }
}
