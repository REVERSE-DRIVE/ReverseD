using UnityEngine;

namespace RoomManage
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "SO/Stage/RoomPack")]
    public class RoomPack : ScriptableObject
    {
        public int id;
        public string packName;
        public RoomSO[] rooms;

        public RoomSO RandomPickMap()
        {
            return rooms[Random.Range(1, rooms.Length)];
            
        }
    }
}