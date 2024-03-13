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
public class RoomSO : ScriptableObject
{
    public RoomType roomType;
    public GameObject mapPrefab;
    public float generateRate = 1f;

}
