using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomManage
{
    
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomType _roomType;
        [SerializeField] private bool isCleared;
        [SerializeField] private float playerDetectDistance = 13;

        [SerializeField] private Phase[] _phases;

        private Transform enemyParent;
        [SerializeField] private int currentPhase = 0;
        [SerializeField] private bool isPhaseStarted;
        public int currentEnemyAmount
        {
            get
            {
                return enemyParent.childCount;
            }
        }

        private void Awake()
        {
            enemyParent = transform.Find("EnemyParent");
        }

        private void Update()
        {
            if (!isCleared)
            {
                CheckPlayer();
            }
        }

        private void CheckPlayer()
        {
            if (!isPhaseStarted)
            {
                if (Vector2.Distance(GameManager.Instance._PlayerTransform.position, transform.position) <=
                    playerDetectDistance - 1)
                {
                    StartPhase();
                }

            }
            else
            {// 페이즈가 클리어 되었는지 확인
                CheckClear();

            }
        }

        public void StartPhase()
        {
            GenerateEnemy();
            isPhaseStarted = true;
        }
    
        private void GenerateEnemy()
        {
            GameManager.Instance._RoomManager.GeneratePhase(enemyParent, playerDetectDistance-1, _phases[currentPhase]);
            
        }

        private void CheckClear()
        {
            if (currentEnemyAmount == 0)
            {
                currentPhase++;
                isPhaseStarted = false;
            }
            if (currentPhase >= _phases.Length)
            {
                Debug.Log("스테이지 클리어");
                isCleared = true;
                 

            }
        }
    }
}
