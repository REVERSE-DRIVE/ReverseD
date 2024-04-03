using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
    
}
public class BeamProjecter : MonoBehaviour
{
    [SerializeField] private int reflection = 0;
    [SerializeField] private Beam beam;
    [SerializeField] private Direction shootDirection;

    [SerializeField] private bool onDirectionRefresh;

    private void Awake()
    {
        SetBeam();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onDirectionRefresh)
        {
            beam.direction = FindDirection(shootDirection);
            
        }
    }

    private void SetBeam()
    {
        beam.reflection = reflection;
        beam.direction = FindDirection(shootDirection);
        
    }

    private Vector2 FindDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.UpRight:
                return new Vector2(1,1);
            case Direction.Right:
                return Vector2.right;
            case Direction.DownRight:
                return new Vector2(1, -1);
            case Direction.Down:
                return Vector2.down;
            case Direction.DownLeft:
                return new Vector2(-1,-1);
            case Direction.Left:
                return Vector2.left;
            case Direction.UpLeft:
                return new Vector2(-1,1);
            default:
                return Vector2.zero;
        }
    }
    
    
}
