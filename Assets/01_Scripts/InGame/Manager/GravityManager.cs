using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GravityManager
{
    public static Vector2 gravityDirection { get; private set; }
    public static float gravityScale { get; private set; }
    private static GravityDirectionEnum _currentDirection;

    public static bool isOnGravity { get; private set; } = false;



    private void Update()
    {
        
    }

    
    
    public static void SetGravityDirection(GravityDirectionEnum direction)
    {
        switch (direction)
        {
            case GravityDirectionEnum.Up:
                gravityDirection = Vector2.up;
                break;
            case GravityDirectionEnum.Down:
                gravityDirection = Vector2.down;
                break;
            case GravityDirectionEnum.Left:
                gravityDirection = Vector2.left;
                break;
            case GravityDirectionEnum.Right:
                gravityDirection = Vector2.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public void OffGravity()
    {
        _gravityDirection = Vector2.zero;
    }
}
