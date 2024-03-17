using System.Collections.Generic;
using UnityEngine;

namespace ItemManage
{
    [CreateAssetMenu(menuName = "SO/Item/Items")]
    public class Items : ScriptableObject
    {
        public List<ItemData> itemDataList;
    }
}
