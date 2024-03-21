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
        [FormerlySerializedAs("dataChipType")] public ResourceType resourceType;
        public float mass;

        /**
         * <summary>
         * ItemData에 따라 DataPack인지 DataChip인지 판별하여 설정
         * </summary>
         */
        public void SetType()
        {
            if (itemType == ItemType.Package)
            {
                resourceType = ResourceType.Null;
            }
            else
            {
                packageType = PackageType.Null;
            }
        }
    }
}