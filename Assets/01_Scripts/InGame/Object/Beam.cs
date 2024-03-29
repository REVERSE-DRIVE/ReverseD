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
        hit = RayManager.Ray(transform.position, direction, rayShootDistance, wallLayerMask);
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
        points[1] = RayManager.RayPoint(points[0], direction, rayShootDistance, wallLayerMask);
        Vector2 previousPoint = points[1];
        Vector2 outDir = RayManager.Reflect(points[0], direction, rayShootDistance, wallLayerMask);
        bool isDisconnected = false;
        for (int i = 2; i < points.Length; i++)
        {
            if (isDisconnected)
            {
                points[i] = previousPoint; 
            }
            
            if (RayManager.Ray(previousPoint, outDir, rayShootDistance, wallLayerMask).collider != null)
            {
                points[i] = RayManager.RayPoint(previousPoint, outDir, rayShootDistance, wallLayerMask);
                
                outDir = RayManager.Reflect(previousPoint, outDir, rayShootDistance, wallLayerMask);
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

}