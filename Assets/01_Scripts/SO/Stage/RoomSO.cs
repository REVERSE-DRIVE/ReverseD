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
[CreateAssetMenu(menuName = "SO/Stage/Room")]
[System.Serializable]
public class RoomSO : ScriptableObject
{
    public RoomType roomType;
    public GameObject mapPrefab;
    public float generateRate = 1f;
    [Range(0,4)]
    public int maxPath = 1;
}
