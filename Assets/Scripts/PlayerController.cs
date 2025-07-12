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
    
    private readonly int _moveSpeedParameter = Animator.StringToHash("MoveSpeed");
    private readonly int _isJumpingParameter = Animator.StringToHash("IsJumping");
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private float _moveInput;
    private readonly float _checkRadius = 0.3f;
    private bool _isGrounded = true;
    private bool _isFacingRight = true;
    
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
        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
        _animator.SetFloat(_moveSpeedParameter, Mathf.Abs(_moveInput));    
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPosition.position, _checkRadius, whatIsGround);
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger(_isJumpingParameter);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            var coinSpawner = GameObject.Find("CoinSpawner");
            if (!coinSpawner) return;
            coinSpawner.GetComponent<CoinSpawner>().CollectCoin(collision.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        var coinAudioPlayer = GameObject.FindGameObjectWithTag("CoinAudioPlayer");
        if (!coinAudioPlayer) return;
        
        var audioSource = coinAudioPlayer.GetComponent(typeof(AudioSource));
        if (audioSource)
        {
            audioSource.GetComponent<AudioSource>().Play();
        }
        
        
        
        Destroy(coin);
    }
    
}
