using UnityEngine;
using ItemManage;
using UnityEngine.Serialization;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/Item/ItemData")]
    [System.Serializable]
    public class ItemData: ScriptableObject
    {
        public int id;
        public Rank itemRank;
        public string itemName;
        public string description;
        public Sprite icon;
        public ItemType itemType;
        public PackageType packageType;
        public ResourceType resourceType;
        public ProtocolType protocolType;
        public bool isPiece;
        public float mass;
        
        public void OnEnable()
        {
            if (itemType == ItemType.Package)
            {
                resourceType = ResourceType.Null;
                protocolType = ProtocolType.Null;
            }
            else if (itemType == ItemType.Resource)
            {
                packageType = PackageType.Null;
                protocolType = ProtocolType.Null;
            }
            else if (itemType == ItemType.Protocol)
            {
                packageType = PackageType.Null;
                resourceType = ResourceType.Null;
            }
        }
    }
}