using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyManage
{
    // 대충 아무 AI나 쓸때 사용
    public abstract class EnemyAI : MonoBehaviour
    {
        protected Transform _playerTrm;

        protected Enemy _enemyBase;
        [Header("Current State Values")]
        [Space(10)]
        [SerializeField] protected bool _isStatic;
       
        
        
        protected Rigidbody2D _rigid;
        protected LayerMask _playerLayer;


        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _enemyBase = GetComponent<Enemy>();
            _playerLayer = LayerMask.GetMask("Player");
        }

        protected virtual void Start()
        {
            _playerTrm = GameManager.Instance._PlayerTransform;

        }
        
        
        

        
    }
}
