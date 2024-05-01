using EntityManage;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    #region Component

    private Transform _visualTrm;
    private Player _player;
    [HideInInspector] public Rigidbody2D rigid;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private ParticleSystem _walkParticle;

    #endregion
    
    [SerializeField] 
    private Vector3 _direction;

    [SerializeField]
    private bool flipX;
    private bool _isWalking;
    private bool _isStop;
    private bool _isMoving;

    private int _moveBoolHash;
    
    public VariableJoystick Joystick 
    { 
        get => _joystick;
        set => _joystick = value;
    }

    public Vector2 GetInputVec => _direction;
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _visualTrm = transform.Find("Visual");
        _spriteRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        _animator = _visualTrm.GetComponent<Animator>();
        _moveBoolHash = Animator.StringToHash("IsMoving");
        
        rigid = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();

        _walkParticle = transform.Find("FootStep").GetComponent<ParticleSystem>();
        _player.OnPlayerDieEvent += HandlePlayerDie;
    }


    private void Start()
    {
        PlayerManager.Instance.UpdateStat();
    }

    void Update()
    {
        if(_isMoving && !_isWalking)
        {
            _isWalking = true;
            _walkParticle.Play();
        }else if (!_isMoving && _isWalking)
        {
            _isWalking = false;
            _walkParticle.Stop();
        }
        Move();
        Rotate();
    }


    private void Move()
    {
        _isMoving = rigid.velocity.magnitude > 0.1f;
        _animator.SetBool(_moveBoolHash, _isMoving);
        if (TimeManager.TimeScale == 0) return;
        Vector2 dir = _joystick.Input;
        if (dir.magnitude > 0.1f)
        {
            _isStop = true;
            _direction = dir.normalized;
            rigid.velocity = _direction * (_player.MoveSpeed * TimeManager.TimeScale);
        }
        else if(_isStop)
        {
            _isStop = false;
            rigid.velocity = Vector2.zero;
            
        }
    }
    
    private void Rotate()
    {
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    
    public void SaveDirection()
    {
        flipX = _spriteRenderer.flipX;
    }

    public void ChangeSprite()
    {
        
    }

    public void ImmediatelyStop()
    {
        rigid.velocity = Vector2.zero;
    }
    
    
    private void HandlePlayerDie()
    {
        rigid.velocity = Vector2.zero;
        _walkParticle.Stop();
    }
}