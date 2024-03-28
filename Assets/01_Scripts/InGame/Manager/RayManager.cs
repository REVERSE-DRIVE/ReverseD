using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayManager
{
    
    public static RaycastHit2D Ray(Vector2 origin, Vector2 dir)
    {
        return Physics2D.Raycast(origin, dir);
        
    }
    
    public static RaycastHit2D Ray(Vector2 origin, Vector2 dir, float rayShootDistance)
    {
        return Physics2D.Raycast(origin, dir, rayShootDistance);
        
    }
    
    
    public static RaycastHit2D Ray(Vector2 origin, Vector2 dir, float rayShootDistance, LayerMask layerMask)
    {
        return Physics2D.Raycast(origin, dir, rayShootDistance, layerMask);
        
    }
    
    /**
     * <summary>
     * 반사 벡터를 반환한다
     * </summary>
     * <returns>
     * 반사 벡터
     * </returns>>
     */
    public static Vector2 Reflect(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Ray(origin, dir);

        if (hit.collider != null)
        {
            //print("반사됨 ");
            Vector2 incomingVector = dir;
            Vector2 normalVector = hit.normal;
            Vector2 reflectVector = Vector2.Reflect(incomingVector, normalVector);
            return reflectVector;
        }
        return dir;
    } 
}
