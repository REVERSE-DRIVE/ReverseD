using System;
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

        private ParticleSystem phaseStartParticle;
        [SerializeField]
        private WallOnPath[] walls;
        
        
        
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
            phaseStartParticle = transform.Find("MapActiveParticle").GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            walls = transform.Find("Paths").GetComponentsInChildren<WallOnPath>();

        }

        private void Update()
        {
            if (!isCleared)
            {
                CheckPlayer();
            }

            if (!isPhaseStarted)
            {
                OpenAllDoor();
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
                    CloseAllDoor();
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
            phaseStartParticle.Play();
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
                GameManager.Instance._UIManager.ShowRoomClear();
                OpenAllDoor();
                
            }
        }

        private void CloseAllDoor()
        {
            for (int i = 0; i < 4; i++)
            {
                walls[i].OnWall();
            }
        }

        private void OpenAllDoor()
        {
            for (int i = 0; i < 4; i++)
            {
                walls[i].SetWall();
            }
        }
    }
}
