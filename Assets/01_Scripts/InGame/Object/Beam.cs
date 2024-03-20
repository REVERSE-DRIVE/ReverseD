using System;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [Header("Setting Values")]
    public int reflection = 0;
    public Vector2 direction;
    public float rayShootDistance = 30;

    [Header("Else")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private bool onLaser;
    //public bool refreshLine = true;
    private float time;

    
    [SerializeField]
    private Vector2[] points;
    private RaycastHit2D hit;
    
    
    private void Start()
    {
        _lineRenderer.positionCount = reflection + 2;
        points = new Vector2[reflection+2];
    }

    private void Update()
    {
        hit = Ray(transform.position, direction);
        onLaser = hit;
        _lineRenderer.enabled = hit;
        if (onLaser)
        {
            
            if (!hit) return;

            ShootLaser();
            DrawLaserLine();
            
        }
    }

    private void ShootLaser()
    {
        points[0] = transform.position;
        points[1] = RayPoint(points[0], direction);
        Vector2 previousPoint = points[1];
        Vector2 outDir = Reflect(points[0], direction);
        bool isDisconnected = false;
        for (int i = 2; i < points.Length; i++)
        {
            if (isDisconnected)
            {
                points[i] = previousPoint; 
            }
            
            if (Ray(previousPoint, outDir).collider != null)
            {
                points[i] = RayPoint(previousPoint, outDir);
                
                outDir = Reflect(previousPoint, outDir);
                previousPoint = points[i];

            }
            else
            {
                isDisconnected = true;
            }
            
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
        return Physics2D.Raycast(origin, dir, rayShootDistance, wallLayerMask);
        
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
    
    public Vector2 RayPoint(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Ray(origin, dir);
        // 레이 자체가 문제인듯
        if (hit.collider != null)
        {
            return hit.point;
        }
        print("RayPoint ");
        return Vector2.zero;
    } 
}