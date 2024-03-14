using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab; // 방 프리팹
    public GameObject horizontalPathPrefab; // 가로로 난 길 프리팹
    public GameObject verticalPathPrefab; // 세로로 난 길 프리팹
    public GameObject horizontalWallPrefab; // 가로로 막힌 벽 프리팹
    public GameObject verticalWallPrefab; // 세로로 막힌 벽 프리팹

    public int minRooms = 5; // 최소 방 개수
    public int maxRooms = 10; // 최대 방 개수
    public float roomSize = 10f; // 방의 가로 및 세로 길이
    public float pathLength = 16f; // 길의 길이

    private List<GameObject> rooms = new List<GameObject>(); // 생성된 방 리스트

    void Start()
    {
        GenerateRooms();
    }

    [ContextMenu("DebugGenerateRooms")]
    public void GenerateRooms()
    {
        int numRooms = Random.Range(minRooms, maxRooms + 1);

        // 초기 방 생성
        GameObject firstRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
        rooms.Add(firstRoom);

        // 방 연결
        GameObject previousRoom = firstRoom;
        for (int i = 1; i < numRooms; i++)
        {
            GameObject newRoom = Instantiate(roomPrefab, GetNextRoomPosition(previousRoom), Quaternion.identity);
            rooms.Add(newRoom);
            ConnectRooms(previousRoom, newRoom);
            previousRoom = newRoom;
        }
    }

    Vector3 GetNextRoomPosition(GameObject previousRoom)
    {
        Vector3 exitDirection = GetRandomExitDirection(previousRoom);
        return previousRoom.transform.position + exitDirection * (roomSize + pathLength);
    }

    Vector3 GetRandomExitDirection(GameObject room)
    {
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        Vector3 oppositeDirection = -GetRandomExitDirection(room, directions);

        Vector3 exitDirection;
        do
        {
            exitDirection = directions[Random.Range(0, directions.Length)];
        } while (exitDirection == oppositeDirection);

        return exitDirection;
    }

    private Vector3 GetRandomExitDirection(GameObject room, Vector3[] directions)
    {
        Vector3 exitDirection = Vector3.zero;

        foreach (var direction in directions)
        {
            Vector3 exitPosition = room.transform.position + direction * (roomSize * 0.5f + pathLength * 0.5f);
            Collider[] hitColliders = Physics.OverlapSphere(exitPosition, 0.1f);
            if (hitColliders.Length == 0)
            {
                exitDirection = direction;
                break;
            }
        }

        return exitDirection;
    }

    void ConnectRooms(GameObject room1, GameObject room2)
    {
        Vector3 exitDirection = GetRandomExitDirection(room1);
        Vector3 oppositeExitDirection = -exitDirection;

        Vector3 exitPosition = room1.transform.position + exitDirection * (roomSize * 0.5f);
        Vector3 entrancePosition = room2.transform.position + oppositeExitDirection * (roomSize * 0.5f);

        if (exitDirection == Vector3.up || exitDirection == Vector3.down)
        {
            Instantiate(verticalPathPrefab, (exitPosition + entrancePosition) * 0.5f, Quaternion.identity);
            Instantiate(verticalWallPrefab, entrancePosition, Quaternion.identity);
        }
        else
        {
            Instantiate(horizontalPathPrefab, (exitPosition + entrancePosition) * 0.5f, Quaternion.identity);
            Instantiate(horizontalWallPrefab, entrancePosition, Quaternion.identity);
        }
    }
}