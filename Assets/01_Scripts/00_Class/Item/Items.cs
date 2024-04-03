using System.Collections.Generic;
using UnityEngine;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/Item/Items")]
    public class Items : ScriptableObject
    {
        public List<ItemData> itemDataList;

        /**
         * <param name="id">
         * 찾을 아이템 데이터의 ID
         * </param>
         * <summary>
         * 아이템 아이디에 해당하는 데이터를 찾아준다 
         * </summary>
         */
        public ItemData FindItem(int id)
        {
            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i].id == id)
                {
                    return itemDataList[i];
                }
            }
            Debug.Log("<color='red'>[FindItem] 찾을 수 없는 아이디입니다 (id :"+id+")</color>");
            return null;
        }
        
        /**
         * <param name="protocolType">
         * 찾을 아이템 데이터의 프로토콜 타입
         * </param>
         * <summary>
         * 프로토콜 타입에 해당하는 데이터를 찾아준다
         * </summary>
         */
        public ItemData FindItem(ProtocolType protocolType)
        {
            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i].protocolType == protocolType)
                {
                    return itemDataList[i];
                }
            }
            Debug.Log("<color='red'>[FindItem] 찾을 수 없는 프로토콜 타입입니다 (protocolType :"+protocolType+")</color>");
            return null;
        }
        
        /**
         * <param name="items">
         * 아이템 데이터 배열
         * </param>
         * <param name="protocolType">
         * 프로토콜 타입
         * </param>
         * <summary>
         * 아이템 데이터 배열이 프로토콜 타입에 맞는지 확인
         * </summary>
         */
        public bool IsAcceptCombination(ItemData[] items, ProtocolType protocolType)
        {
            if (items.Length != 3) return false;
            int count = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].protocolType == protocolType && items[i].isPiece)
                {
                    count++;
                }
            }
            if (count == 3)
            {
                return true;
            }
            return false;
        }
    }
}
