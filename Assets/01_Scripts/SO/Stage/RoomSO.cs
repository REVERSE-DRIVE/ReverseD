using UnityEngine;

namespace RoomManage
{
    
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
        public int maxSpawnAmount = 1;
        public int minSpawnAmount;
    }

}