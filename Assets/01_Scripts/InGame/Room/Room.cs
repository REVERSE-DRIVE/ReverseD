using UnityEngine;

namespace RoomManage
{
    
    public class Room : MonoBehaviour
    {
        public int id;
        [SerializeField] private RoomType _roomType;
        [SerializeField] private bool isCleared;
        [SerializeField] private float playerDetectDistance = 13;

        [SerializeField] private Phase[] _phases;

        private Transform _enemyParent;
        [SerializeField] private int currentPhase = 0;
        [SerializeField] private bool isPhaseStarted;

        private ParticleSystem phaseStartParticle;
        [SerializeField]
        private WallOnPath[] walls;

        private SoundObject _soundObject;
        
        
        
        
        public int currentEnemyAmount => _enemyParent.childCount;

        private void Awake()
        {
            _enemyParent = transform.Find("EnemyParent");
            phaseStartParticle = transform.Find("MapActiveParticle").GetComponent<ParticleSystem>();
            _soundObject = GetComponent<SoundObject>();

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

        public void SetClear(bool value)
        {
            isCleared = value;
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
            _soundObject.PlayAudio(0);
        }
    
        /**
         * <summary>
         * 방에서 적들을 스폰해주는 함수
         * </summary>
         */
        private void GenerateEnemy()
        {
            _soundObject.PlayAudio(1);
            GameManager.Instance._RoomManager.GeneratePhase(_enemyParent, playerDetectDistance-1, _phases[currentPhase]);
            
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
                isCleared = true;
                GameManager.Instance._UIManager.ShowRoomClear();
                OpenAllDoor();
                GameManager.Instance.Infect(Random.Range(2, 4));
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
