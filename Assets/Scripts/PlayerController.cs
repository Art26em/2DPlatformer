using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LayerMask))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask whatIsGround;
    
    private readonly int _moveSpeedParameterHash = AnimationParameters.MoveSpeedParamHash;
    private readonly int _isJumpingParameterHash = AnimationParameters.IsJumpingParameter;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private float _moveInput;
    private readonly float _checkRadius = 0.3f;
    private bool _isGrounded = true;
    private bool _isFacingRight = true;
    private string _horizontalAxis = InputAxes.Horizontal;
    
    private readonly float _xFlipScale = -1f;
    private readonly float _yFlipScale = 1f;
    private readonly float _zFlipScale = 1f;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        if (_moveInput > 0 && !_isFacingRight || _moveInput < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        _moveInput = Input.GetAxis(_horizontalAxis);
        _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
        _animator.SetFloat(_moveSpeedParameterHash, Mathf.Abs(_moveInput));    
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(
            transform.localScale.x * _xFlipScale, 
            transform.localScale.y * _yFlipScale, 
            transform.localScale.z * _zFlipScale);
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPosition.position, _checkRadius, whatIsGround);
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger(_isJumpingParameterHash);
        }
    }
    
}
