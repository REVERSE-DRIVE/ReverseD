﻿using UnityEngine;
using ItemManage;

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
        public DataPackType dataPackType;
        public DataChipType dataChipType;
        public float mass;

        /**
         * <summary>
         * ItemData에 따라 DataPack인지 DataChip인지 판별하여 설정
         * </summary>
         */
        public void SetType()
        {
            if (itemType == ItemType.DataPack)
            {
                dataChipType = DataChipType.Null;
            }
            else
            {
                dataPackType = DataPackType.Null;
            }
        }
    }
}