using UnityEngine;

[RequireComponent ( typeof ( Rigidbody2D ) )]
[RequireComponent ( typeof ( SpriteRenderer ) )]
[RequireComponent ( typeof ( Animator ) )]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Speed = nameof(Speed);
    private const string OnGround = nameof(OnGround);
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    
    private bool _isTouchingGround;
    private Rigidbody2D _player;
    private SpriteRenderer _playerRenderer;
    private Animator _playerAnimation;
    
    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        JumpUp();
    }

    private void Move()
    {
        float direction = Input.GetAxis(Horizontal);
        float distance = direction * _moveSpeed * Time.deltaTime;
        
        _playerAnimation.SetFloat(Speed, Mathf.Abs(direction));
        
        transform.Translate(distance * Vector2.right);
        
        if (direction > 0) FlipHorizontal(false);
        else if (direction < 0) FlipHorizontal(true);
    }

    private void JumpUp()
    {
        _playerAnimation.SetBool(OnGround, !_isTouchingGround);
        
        if (Input.GetAxis(Jump) > 0f && IsGrounded())
        {
            _player.velocity = new Vector2(_player.velocity.x, _jumpHeight);
        }
    }

    private bool IsGrounded()
    {
       return _isTouchingGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayerMask);
    }

    private void FlipHorizontal(bool value)
    {
        _playerRenderer.flipX = value;
    }
}
