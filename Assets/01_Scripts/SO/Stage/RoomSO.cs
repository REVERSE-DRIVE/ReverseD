using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomManage
{
    public enum PathDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public enum RoomType
    {
        Start,
        Load,
        NormalRoom,
        SpecialRoom,
        End
    }


    [CreateAssetMenu(menuName = "SO/Stage/Room")]
    [System.Serializable]
    public class RoomSO : ScriptableObject
    {
        public RoomType roomType;
        public GameObject mapPrefab;
        public float roomSpawnRate = 1f;
        
    }

}