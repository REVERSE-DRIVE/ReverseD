using System;
using System.Collections;
using System.Collections.Generic;
using SaveDatas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoomManage
{

    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private Transform grid;
        public RoomPack roomPack; // 방 프리팹들의 베이스
        public GameObject horizontalPathPrefab; // 가로로 난 길 프리팹
        public GameObject verticalPathPrefab; // 세로로 난 길 프리팹

        public int minRooms = 5; // 최소 방 개수
        public int maxRooms = 10; // 최대 방 개수
        public float roomSize = 17f; // 방의 가로 및 세로 길이
        public float pathLength = 16f; // 길의 길이
        public float HLoadXOffset;
        public float VLoadYOffset;
        public static Action WallGenerateEvent;
        [SerializeField]
        private List<GameObject> rooms = new List<GameObject>(); // 생성된 방 리스트


        public GameObject LastRoom => rooms[^1];
        public GameObject FirstRoom => rooms[0];
        

        public void DelayAction(Action action, float time)
        {
            StartCoroutine(Delay(action, time));
        }

        private IEnumerator Delay(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        #region Map Loading

        
        // public void LoadRoomData(RoomData[] roomDatas, RoadData[] roadDatas)
        // {
        //     // 저장된 맵 파일을 열어야함
        //     for (int i = 0; i < roomDatas.Length; i++)
        //     {
        //         RoomSO room = roomPack.FindMap(roomDatas[i].roomID);
        //         GameObject newRoom = Instantiate(room.MapPrefab, roomDatas[i].roomPosition, Quaternion.identity);
        //         if(roomDatas[i].isRoomCleared)
        //             newRoom.GetComponent<Room>().SetClear(roomDatas[i].isRoomCleared);
        //         rooms.Add(newRoom);
        //
        //     }
        //
        //     for (int i = 0; i < roadDatas.Length; i++)
        //     {
        //         if (roadDatas[i].isRoadHorizontal)
        //         { // 가로 길 생성
        //             Instantiate(horizontalPathPrefab, roadDatas[i].roadPosition, Quaternion.identity);
        //         }
        //         else
        //         { // 세로길 생성
        //             Instantiate(verticalPathPrefab, roadDatas[i].roadPosition, Quaternion.identity);
        //         }
        //     }
        //     
        // }

        #endregion
        

        #region Map Generating
        
        public void ResetMap()
        {
            DeleteRooms();
            GenerateRooms();
        }

        public void DeleteRooms()
        {            
            WallGenerateEvent = null;
            rooms = new List<GameObject>();
            foreach (Transform child in grid)
            {
                Destroy(child.gameObject);
            }

        }

        public void GenerateRooms()
        {
            int numRooms = Random.Range(minRooms, maxRooms + 1);

            // 초기 방 생성
            GameObject firstRoom = Instantiate(roomPack.rooms[0].MapPrefab, Vector2.zero, Quaternion.identity);
            firstRoom.transform.SetParent(grid);
            rooms.Add(firstRoom);

            // 방 연결
            GameObject previousRoom = firstRoom;
            for (int i = 1; i < numRooms; i++)
            {
                GameObject newRoom = Instantiate(roomPack.RandomPickMap().MapPrefab, GetNextRoomPosition(previousRoom), Quaternion.identity);
                newRoom.transform.SetParent(grid);
                rooms.Add(newRoom);
                ConnectRooms(previousRoom, newRoom);
                previousRoom = newRoom;
            }
            DelayAction(WallGenerateEvent, 0.1f);
        }

        
        private Vector3 GetNextRoomPosition(GameObject previousRoom)
        {
            Vector3 exitDirection = GetRandomExitDirection(previousRoom);
            Vector3 result = previousRoom.transform.position + exitDirection * (roomSize + pathLength);
            int duplication = 0;

            while (true)
            {
                duplication = 0;
                exitDirection = GetRandomExitDirection(previousRoom);
                result = previousRoom.transform.position + exitDirection * (roomSize + pathLength);
                
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (Vector2.Distance(rooms[i].transform.position, result) == 0)
                    {
                        duplication++;
                    }
                }
                if (duplication == 0)
                {
                    break;
                }
            }
            return result;
        }

        private Vector2 GetRandomExitDirection(GameObject room)
        {
            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
            Vector2 oppositeDirection = -GetRandomExitDirection(room, directions);

            Vector2 exitDirection;
            do
            {
                exitDirection = directions[Random.Range(0, directions.Length)];
            } while (exitDirection == oppositeDirection);

            return exitDirection;
        }

        private Vector2 GetRandomExitDirection(GameObject room, Vector3[] directions)
        {
            Vector2 exitDirection = Vector3.zero;

            foreach (Vector3 direction in directions)
            {
                Vector2 exitPosition = room.transform.position + direction * (roomSize * 0.5f + pathLength * 0.5f);
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(exitPosition, 0.1f);
                if (hitColliders.Length == 0)
                {
                    exitDirection = direction;
                    break;
                }
            }

            return exitDirection;
        }

        private void ConnectRooms(GameObject room1, GameObject room2)
        {
            Vector3 exitDirection = GetRandomExitDirection(room1);
            Vector3 oppositeExitDirection = -exitDirection;

            Vector3 exitPosition = room1.transform.position + exitDirection * (roomSize * 0.5f);
            Vector3 entrancePosition = room2.transform.position + oppositeExitDirection * (roomSize * 0.5f);

            if (room1.transform.position.x == room2.transform.position.x) // 같은 열에 있는 경우
            {
                GameObject load = Instantiate(verticalPathPrefab,
                    (exitPosition + entrancePosition) * 0.5f + Vector3.down * 0.5f, Quaternion.identity);
                Vector2 pos = load.transform.position;
                load.transform.position = new Vector2(pos.x, pos.y + VLoadYOffset);
                load.transform.SetParent(grid);

            }
            else if (room1.transform.position.y == room2.transform.position.y) // 같은 행에 있는 경우
            {
                GameObject load = Instantiate(horizontalPathPrefab,
                    (exitPosition + entrancePosition) * 0.5f + Vector3.left * 0.5f, Quaternion.identity);
                Vector2 pos = load.transform.position;
                load.transform.position = new Vector2(pos.x + HLoadXOffset, pos.y);
                load.transform.SetParent(grid);
            }
        }
        
        #endregion

    }
}