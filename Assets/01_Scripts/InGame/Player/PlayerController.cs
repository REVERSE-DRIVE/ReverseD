using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    
    private SpriteRenderer _spriteRenderer;
    private PlayerAttack _playerAttack;
    private Rigidbody2D _rigid;
    private Player _player;
    private Vector3 dir;
    private bool isMoving;

    private bool flipX;
    
    
    public VariableJoystick Joystick 
    { 
        get => _joystick;
        set => _joystick = value;
    }
    
    public bool IsMoving => isMoving;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAttack = GetComponent<PlayerAttack>();
        _rigid = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        PlayerManager.Instance.UpdateStat();
    }

    void Update()
    {
        Move();
        Rotate();
    }


    private void Move()
    {
        isMoving = _rigid.velocity.magnitude > 0.1f;
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        _rigid.velocity = direction.normalized * (PlayerManager.Instance.setting_moveSpeed * TimeManager.TimeScale);
    }
    
    private void Rotate()
    {
        dir = _joystick.Direction.normalized;
        _spriteRenderer.flipX = isMoving ? dir.x > 0 : flipX;
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