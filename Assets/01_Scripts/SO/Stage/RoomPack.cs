using UnityEngine;
using UnityEngine.Rendering;

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

        public RoomSO FindMap(int id)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].id == id)
                {
                    return rooms[i];
                }
            }
            Debug.LogError($"Cant Find RoomData! [id : {id}]");
            return null;
        }
    }
}