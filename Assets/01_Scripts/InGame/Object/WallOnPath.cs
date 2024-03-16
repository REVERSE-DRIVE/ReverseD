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

        private void OnEnable()
        {
            RoomGenerator.WallGenerateEvent += SetWall;
        }

        private void OnDestroy()
        {
            RoomGenerator.WallGenerateEvent -= SetWall;
        }


        public void SetWall()
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(
                transform.position, 5, LayerMask.GetMask("LoadGround"));
            if (hit.Length == 0)
            {
                wall.gameObject.SetActive(true);
            }
            else
            {
                wall.gameObject.SetActive(false);
            }
            
        }
        
    }
}
