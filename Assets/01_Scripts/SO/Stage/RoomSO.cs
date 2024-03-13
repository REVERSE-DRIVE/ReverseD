using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Start,
    Load,
    NormalRoom,
    SpecialRoom,
    End
}

public enum PathType
{
    Horizontal,
    Vertical
}

public enum PathDirection
{
    Up,
    Down,
    Left,
    Right 
}

[System.Serializable]
public struct Path
{
    public Transform pathTrm;
    public PathType pathType;
    public PathDirection pathDirection;
}
[CreateAssetMenu(menuName = "SO/Stage/Room")]
[System.Serializable]
public class RoomSO : ScriptableObject
{
    public RoomType roomType;
    public GameObject mapPrefab;
    public float generateRate = 1f;
    public Path[] paths;

    public int pathAmount
    {
        get { return paths.Length; }
        private set { }
    }
}
