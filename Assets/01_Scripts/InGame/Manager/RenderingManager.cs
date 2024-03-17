using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingManager : MonoBehaviour
{
    [SerializeField] private float renderingDistance = 40;
    [SerializeField] private Transform mapGrid;

    private bool onRendering;

    private void Awake()
    {
        if (mapGrid == null)
            mapGrid = GameObject.Find("Grid").transform;

    }

    public void _Start()
    {
        onRendering = true;
        

    }

    private void Update()
    {
        if (onRendering)
        {
            foreach (Transform map in mapGrid)
            {
                if (IsCancelRendering(map.position))
                {
                    map.gameObject.SetActive(false);
                }
                else
                {
                    map.gameObject.SetActive(true);
                }
            }
        }
    }

    public float DistanceToPlayer(Vector2 position)
    {
        return Vector2.Distance(GameManager.Instance._PlayerTransform.position, position); ;
    }
    
    public float DistanceToCamera(Vector2 position)
    {
        return Vector2.Distance(Camera.main.transform.position, position);
    }

    public bool IsCancelRendering(Vector2 position)
    {
        return DistanceToCamera(position) > renderingDistance;
    }
    
}
