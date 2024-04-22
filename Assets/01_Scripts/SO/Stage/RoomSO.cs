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
        public int id;
        public RoomType roomType;
        [SerializeField]
        private GameObject mapPrefab;

        public GameObject MapPrefab
        {
            get
            {
                mapPrefab.GetComponent<Room>().id = id;
                return mapPrefab;
            }
        }
        
        public float roomSpawnRate = 1f;
        public int maxSpawnAmount = 1;
        public int minSpawnAmount;
    }

}