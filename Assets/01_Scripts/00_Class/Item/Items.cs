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
        
        
    }
}
