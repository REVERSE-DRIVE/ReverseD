using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{    
    [SerializeField] private GameObject horizontalWall;
    [SerializeField] private GameObject verticalWall;
    [SerializeField] private GameObject horizontalLoad;
    [SerializeField] private GameObject verticalLoad;


    [SerializeField] private RoomSO[] rooms;

    [SerializeField] private Grid grid;

    
    
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void 

    public void GenerateMap()
    {
        
        
    }
}
