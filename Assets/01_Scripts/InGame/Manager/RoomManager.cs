using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{    
    [SerializeField] private GameObject horizontalWall;
    [SerializeField] private GameObject verticalWall;
    [SerializeField] private GameObject horizontalLoad;
    [SerializeField] private GameObject verticalLoad;
    [SerializeField] private Grid grid;

    [Header("Setting Options")]
    [SerializeField] private RoomSO[] rooms;

    [SerializeField] private int rateDecreaseValue = 7;
    [SerializeField]
    private int _LoadRate = 100;
    private int _roomAmount = 0;
    
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public void Debug_GenerateMap()
    {
        GeneratePath();
    }

    public void MapReset()
    {
        
    }
    
    public void GenerateMap()
    {
        if (_roomAmount == 0)
        {
            //처음 맵 하나 생성
        }

        for (int i = 0; i < UPPER; i++)
        {
            
        }
    }

    private void GeneratePath(RoomSO room)
    {
        Path[] paths = room.paths;
        
        
        for (int i = 0; i < room.pathAmount; i++)
        {
            Path path = paths[i];
            switch (path.pathType)
            {
                case PathType.Horizontal:
                    if (_LoadRate > Random.Range(0, 99))
                    {
                        _LoadRate -= rateDecreaseValue;
                        
                    }
                    else
                    {
                        PoolManager.Get(horizontalWall, path.pathTrm.position, Quaternion.identity);

                    }
                    break;
                    
                case PathType.Vertical:
                    if (_LoadRate > Random.Range(0, 99))
                    {
                        _LoadRate -= rateDecreaseValue;
                    }
                    else
                    {
                        PoolManager.Get(verticalWall, path.pathTrm.position, Quaternion.identity);
                        
                    }
                    break;
            }
            
        }
    }
}
