using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float patrollingTime;
    
    private readonly int _moveSpeedParameter = Animator.StringToHash("MoveSpeed");
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private float _moveInput = 1;
    private float _passedTime;
    private bool _isFacingRight = true;
    
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
        _animator.SetFloat(_moveSpeedParameter, Mathf.Abs(_moveInput));    
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
}
