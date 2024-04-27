using EntityManage;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    #region Component
    
    private Player _player;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _walkParticle;

    #endregion
    
    [SerializeField] 
    private Vector3 _direction;

    [SerializeField]
    private bool flipX;
    private bool _isWalking;
    private bool _isStop;
    private bool _isMoving;
    
    public VariableJoystick Joystick 
    { 
        get => _joystick;
        set => _joystick = value;
    }

    public Vector2 GetInputVec
    {
        get => _direction;
        private set { }
    }
    
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
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
        _isMoving = _rigid.velocity.magnitude > 0.1f;
        if (TimeManager.TimeScale == 0) return;
        Vector2 dir = _joystick.Input;
        if (dir.magnitude > 0.1f)
        {
            _isStop = true;
            _direction = dir.normalized;
            _rigid.velocity = _direction * (_player.MoveSpeed * TimeManager.TimeScale);
        }
        else if(_isStop)
        {
            _isStop = false;
            _rigid.velocity = Vector2.zero;
            
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
        _spriteRenderer.sprite = PlayerManager.Instance.PlayerSprite;
    }
    
    
    private void HandlePlayerDie()
    {
        _rigid.velocity = Vector2.zero;
        _walkParticle.Stop();
    }
}