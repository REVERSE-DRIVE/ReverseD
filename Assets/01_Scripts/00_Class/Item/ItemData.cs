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
        public float mass;

        /**
         * <summary>
         * ItemData에 따라 Type을 설정
         * </summary>
         */
        public void SetType()
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