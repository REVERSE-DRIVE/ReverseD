using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomManage
{
    
    public class WallOnPath : MonoBehaviour
    {
        [SerializeField] private PathDirection pathDirection;

        private Transform wall;

        private void Awake()
        {
            wall = transform.Find("Wall");
            
        }

        public void SetWall()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position, 5, Vector2.zero, 0, LayerMask.GetMask("LaodGround"));
            
            
            
        }
        
    }
}
