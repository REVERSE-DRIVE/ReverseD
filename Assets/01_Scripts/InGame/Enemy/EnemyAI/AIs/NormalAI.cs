using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyManage.AIs
{
    public class NormalAI : EnemyAI
    {
        

        // protected void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.CompareTag("PlayerArrow"))
        //     {
        //         _currentState = EnemyStateEnum.Stun;
        //     }
        // }
        
        
        
        protected override void Attack()
        {
            // 플레이어를 공격함
            print("[!] Attack Player");
        }

        
        
        
    }
}
