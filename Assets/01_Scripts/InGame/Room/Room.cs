using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomManage
{
    
    public class Room : MonoBehaviour
    {
        [SerializeField] private bool isCleared;
        [SerializeField] private float playerDetectDistance = 13;

        [SerializeField] private Phase[] _phases; 

        private void Update()
        {
            if (!isCleared)
            {
            
            }
        }

        private void CheckPlayer()
        {
            Collider2D[] hit =
                Physics2D.OverlapCircleAll(transform.position, playerDetectDistance, LayerMask.GetMask("Default"));
        
        }
    
        public void GenerateEnemy()
        {
        
        
        }

        public void Clear()
        {
            isCleared = true;
        }
    }
}
