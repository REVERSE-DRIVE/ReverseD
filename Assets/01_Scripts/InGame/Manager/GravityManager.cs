#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public delegate void OnGravityChangedEvent(bool isOnGravity, Vector2 direction, float gravityScale = 1);
public static class GravityManager
{
    public static Vector2 gravityDirection { get; private set; }
    public static float gravityScale { get; private set; } = 1;
    private static GravityDirectionEnum _currentDirection;
    public static OnGravityChangedEvent GravityChangedEvent;
    
    public static bool isOnGravity { get; private set; } = false;
#if UNITY_EDITOR
    [MenuItem("Gravity/Up")]
    public static void DebugSetGravityUp()
    {
        SetGravityDirection(GravityDirectionEnum.Up);
    }
    [MenuItem("Gravity/Down")]
    public static void DebugSetGravityDown()
    {
        SetGravityDirection(GravityDirectionEnum.Down);
    }
    [MenuItem("Gravity/Left")]
    public static void DebugSetGravityLeft()
    {
        SetGravityDirection(GravityDirectionEnum.Left);
    }

    [MenuItem("Gravity/Right")]
    public static void DebugSetGravityRight()
    {
        SetGravityDirection(GravityDirectionEnum.Right);
    }

    [MenuItem("Gravity/Off")]
    public static void DebugSetGravityOff()
    {
        OffGravity();
   }
#endif
    
    
    
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
                return;
            
        }
        GravityChangedEvent?.Invoke(true, gravityDirection, gravityScale);
    }

    
    public static void OffGravity()
    {
        gravityDirection = Vector2.zero;
        GravityChangedEvent?.Invoke(false, gravityDirection);
    }
}
