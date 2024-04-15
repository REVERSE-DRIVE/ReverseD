using EnemyManage;
using ItemManage;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDropBase", menuName = "SO/ItemDropBase")]
public class ItemDropBase : ScriptableObject
{
    public ItemDropType itemDropType;
    public Item[] items;
    
    public Item GetRandomItem()
    {
        return items[Random.Range(0, items.Length)];
    }
}
