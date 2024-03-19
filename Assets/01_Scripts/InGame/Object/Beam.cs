using System;
using UnityEngine;

public class Beam : MonoBehaviour
{
        
    [SerializeField] private int reflection = 0;
    [SerializeField] private LineRenderer _lineRenderer;
    //[SerializeField] private LayerMask wallLayerMask = LayerMask.GetMask("Map");
    [SerializeField] private bool onLaser;
    [SerializeField] private float rayShootDistance = 30;
    private float time;

    [SerializeField]
    private Vector2 direction;

    private Vector2[] points;
    private RaycastHit2D hit;
    
    
    private void Start()
    {
        _lineRenderer.positionCount = reflection + 2;
        points = new Vector2[reflection];
    }

    private void Update()
    {
        
        if (onLaser)
        {
            hit = Ray(transform.position, direction);
            onLaser = !hit;
            _lineRenderer.enabled = !hit;
            if (!hit) return;

            ShootLaser();
            DrawLaserLine();
            
        }
    }

    // private void ShootLaser()
    // {
    //     Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
    //
    //     RaycastHit2D hit = Ray(dir);
    //
    //     if (hit.transform == null)
    //     {
    //         onLaser = false;
    //         _lineRenderer.enabled = false;
    //         return;
    //     }
    //     
    //     _lineRenderer.enabled = true;
    //     onLaser = true;
    //     if (onLaser)
    //     {
    //         points[0] = transform.position;
    //         Vector2 outDir = Reflect(dir);
    //         for (int i = 0; i < points.Length; i++)
    //         {
    //             print(outDir);
    //             print(RayPoint(outDir));
    //             hit = Ray(outDir);
    //             if (!hit)
    //             {
    //                 break;
    //             }
    //             points[i] = RayPoint(outDir);
    //             outDir = Reflect(outDir);
    //         }
    //     }
    // }

    private void ShootLaser()
    {
        for (int i = 0; i < points.Length; i++)
        {
            
        }
    }
    
    public void DrawLaserLine()
    {
        if (onLaser)
        {
            
            for (int i = 0; i < points.Length; i++)
            {
                _lineRenderer.SetPosition(i, points[i]);
            }
        }
    }

    private RaycastHit2D Ray(Vector2 origin, Vector2 dir)
    {
        return Physics2D.Raycast(origin, dir, rayShootDistance);
        
    }

    /**
     * <summary>
     * 반사 벡터를 반환한다
     * </summary>
     * <returns>
     * 반사 벡터
     * </returns>>
     */
    public Vector2 Reflect(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Ray(origin, dir);

        if (hit)
        {
            //print("반사됨 ");
            Vector2 incomingVector = dir;
            Vector2 normalVector = hit.normal;
            Vector2 reflectVector = Vector2.Reflect(incomingVector, normalVector);
            return reflectVector;
        }
        return Vector2.zero;
    } 
    
    public Vector2 RayPoint(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Ray(origin, dir);

        if (hit)
        {
            return hit.point;
        }
        return Vector2.zero;
    } 
}