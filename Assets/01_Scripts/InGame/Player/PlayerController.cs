using EntityManage;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigid;
    private Player _player;
    private Vector3 _direction;
    private bool _isMoving;

    private bool flipX;
    private ParticleSystem _walkParticle;
    private bool _isWalking;
    
    
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
        
        _direction = _joystick.Direction.normalized;
        _rigid.velocity = _direction * (_player.MoveSpeed * TimeManager.TimeScale);
        
    }
    
    private void Rotate()
    {
        
        _spriteRenderer.flipX = _isMoving ? _direction.x > 0 : flipX;
    }
    
    public void SaveDirection()
    {
        flipX = _spriteRenderer.flipX;
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = PlayerManager.Instance.PlayerSprite;
    }
}