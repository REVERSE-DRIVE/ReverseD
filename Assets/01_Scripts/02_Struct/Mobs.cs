using UnityEngine;

namespace RoomManage
{
    [System.Serializable]
    public struct Mob
    {
        [Tooltip("적 개체의 아이디")] public int ID;
        
        public int Amount;
        
    }
}