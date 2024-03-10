using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/ItemData")]
[System.Serializable]
public class ItemData: ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    
}