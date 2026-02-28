using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AudioClip jumpSound;
    public float moveSpeed = 10f;                                                         
    public float jumpForce = 6f;
    private Rigidbody2D _rb;
    public bool _isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private LayerMask PlankLayer;

    public bool _wannajump; 
    private float horizontalInput;
    public Transform body;
    public Transform head;

    public float breakRate = 0.90f;
    public float moveLimit = 5f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        PlankLayer = LayerMask.GetMask("platform");
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _wannajump = true;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Flip only the body depending on movement direction
        if (horizontalInput > 0)
        {
            if (body != null)
            {
                Vector3 scale = body.localScale;
                scale.x = Mathf.Abs(scale.x);
                body.localScale = scale;
            }
        }
        else if (horizontalInput < 0)
        {
            if (body != null)
            {
                Vector3 scale = body.localScale;
                scale.x = -Mathf.Abs(scale.x);
                body.localScale = scale;
            }
        }
    }

    void FixedUpdate()
    {


        if (_rb.linearVelocity.magnitude < moveLimit && horizontalInput != 0)
        {
            _rb.AddForce(horizontalInput * moveSpeed * Vector2.right, ForceMode2D.Force);
        }
        fixMove();


        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, PlankLayer);


        if (Input.GetKey(KeyCode.S) && _isGrounded)
        {
            _rb.linearVelocity *= breakRate;
        }


        if (_wannajump)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            soundManagerScript.instance.playSoundClip(jumpSound, transform, 1f);
            _wannajump = false;
        }

        if (_isGrounded && horizontalInput == 0)
        {
            slow();
        }

    }

    void fixMove()
    {
        if (horizontalInput > 0 && _rb.linearVelocity.x < 0)
        {
            _rb.linearVelocityX *= 0.9f;
        }
        else if (horizontalInput < 0 && _rb.linearVelocity.x > 0)
        {
            _rb.linearVelocityX *= 0.9f;
        }
    }
    
    void slow()
    {
        _rb.linearVelocityX *= breakRate;
    }
   
    
}
