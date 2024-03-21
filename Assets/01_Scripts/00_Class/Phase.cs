using UnityEngine;

namespace RoomManage
{
    [System.Serializable]
    public struct Mobs
    {
        
        [Tooltip("적 개체의 아이디")]
        public int ID;

        public int Amount;

    }
    [System.Serializable]
    public class Phase
    {
        public Mobs[] Mobs;
        
        
    }
}