using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float patrollingTime;
    
    private readonly int _moveSpeedParameterHash = AnimationParameters.MoveSpeedParamHash;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private float _moveInput = 1;
    private float _passedTime;
    private bool _isFacingRight = true;
    
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
        if (patrollingTime <= 0) return;
        
        _passedTime += Time.fixedDeltaTime;
        if (_passedTime >= patrollingTime)
        {
            _moveInput *= -1;
            _passedTime = 0;
        }
        
        if (_moveInput > 0 && !_isFacingRight || _moveInput < 0 && _isFacingRight)
        {
            Flip();
        }
        
        Move();
        
    }
    
    private void Move()
    {
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
    
}
