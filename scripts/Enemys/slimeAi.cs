using System;
using System.Collections;
using UnityEngine;

public class slimeAi : MonoBehaviour
{

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip damagedSound;
    private Transform _transform;

    public LayerMask groundLayer;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public Rigidbody2D _rb;
    public GameObject player;
    private float engageDistance = 8f;
    private bool isInRange = false;
    float jumpMultiplyer = 0.5f; 
    private float jumpForce = 6f;
    private float coolDownTime = 2f;
    private bool canJump = true;

    private int damage = 1;

    private mobInfo _mobInfo;

    private PlayerData PlayerDataScript;
    private heartManager heartManager;
    void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>(); 
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerDataScript = player.GetComponent<PlayerData>();
        heartManager = FindFirstObjectByType<heartManager>();
        _mobInfo = GetComponent<mobInfo>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isInRange = Vector3.Distance(_transform.position, player.transform.position) <= engageDistance;
        if (isInRange && isGrounded && canJump)
        {
            SlimeMove();  
        }
            
        setDir();
    }

    void Update()
    {
        if (_mobInfo.currentHealth <= 0)
        {
            _mobInfo.spawnCoin(UnityEngine.Random.Range(1,2));
            _mobInfo.spawnHealthItem();
            Destroy(gameObject);
        }
    }


    private void SlimeMove()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 jumpDirection = (direction + Vector2.up * jumpMultiplyer).normalized;
        _rb.linearVelocity = jumpDirection * jumpForce;
        canJump = false;
        soundManagerScript.instance.playSoundClip(jumpSound, _transform,  1f);
        StartCoroutine(coolDown());
    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        canJump = true; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDataScript.currentHealth -= damage;
            heartManager.UpdateHealth();
        }
    }

    private void setDir()
    {

        switch (_rb.linearVelocity.x)
        {
            case > 0.001f:
            {
                Vector3 scale = transform.localScale;
                scale.x = -1f;
                transform.localScale = scale;
                break;
            }

            case < -0.001f:
            {
                Vector3 scale = transform.localScale;
                scale.x = 1f;
                transform.localScale = scale;
                break;
            }
        }
    }

}
