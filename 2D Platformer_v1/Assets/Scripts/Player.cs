using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    
    private Mover _mover;
    private View _view;
    
    private bool _isTouchingGround;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _view = GetComponent<View>();
    }
    
    private void FixedUpdate()
    {
        float direction = Input.GetAxis(Horizontal);
        
        HandleMovement(direction);
        UpdateAnimation(direction);
        HandleJump();
    }
    
    private void HandleMovement(float direction)
    {
        _mover.Move(direction);
        _mover.Rotate(direction);
    }
    
    private void UpdateAnimation(float direction)
    {
        _view.SetSpeed(Mathf.Abs(direction));
        _view.SetOnGround(IsGrounded());
    }

    private void HandleJump()
    {
        if (Input.GetAxis(Jump) > 0f && IsGrounded())
        {
            _mover.JumpUp();
            _view.SetOnGround(!_isTouchingGround);
        }
    }

    private bool IsGrounded()
    {
        return _isTouchingGround = Physics2D.OverlapCircle
            (_groundCheck.position, _groundCheckRadius, _groundLayerMask);
    }
}
