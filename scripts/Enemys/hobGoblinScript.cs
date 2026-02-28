using UnityEngine;
using System.Collections;


public class hobGoblinScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    
    //movement related
    bool canJump = true;
    public float moveSpeed = 5;
    public float targetDistance = 10;
    public float jumpForce = 10f;
    public float jumpCheckDistance = 0.1f;
    public LayerMask groundLayer;
    Vector2 direction;
    float distance;
    bool isFlipped = false;



    public GameObject slashPrefab;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    private bool canAttack = true;
    public float ad = 1;
    private Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private mobInfo mobInfoScript;

    private bool alive = true;

    public GameObject corpse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(jumpCooldown());
        Vector3 s = transform.localScale;
        mobInfoScript = GetComponent<mobInfo>();
    }

    void Update()
    {
        if (!alive) return;
        direction = player.position - transform.position;
        distance = direction.magnitude;
        targetPlayer();
        jumpSomtimes();   
        if (distance < attackRange)
        {
            attackPlayer();
        }
        if (mobInfoScript.currentHealth <= 0)
        {
            die();
        }
    }

    void targetPlayer()
    {

        if (distance < targetDistance)
        {
            bool isMoving = false;

            if (Mathf.Abs(rb.linearVelocity.x) < moveSpeed)
            {
                rb.AddForce(new Vector2(direction.x * moveSpeed, 0), ForceMode2D.Force);
                isMoving = true;
            }

            if (direction.x > 0)
            {
                Vector3 s = transform.localScale;
                s.x = -Mathf.Abs(s.x);
                transform.localScale = s;
                isFlipped = true;
            }
            else if (direction.x < 0)
            {
                Vector3 s = transform.localScale;
                s.x = Mathf.Abs(s.x);
                transform.localScale = s;
                isFlipped = false;
            }

            animator.SetBool("isMoving", isMoving || Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void jumpSomtimes(){
        if (player.position.y > transform.position.y && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(jumpCooldown());
        }
    }

    IEnumerator jumpCooldown(){
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        canJump = true; 
    }



    void attackPlayer()
    {
        //when in range, toggle child object animation and damaga everything in hitrange of child object
        //should spawn in child object which chould have
        if (canAttack)
        {
            Vector3 spawnpos = transform.position;
            if ( isFlipped )
            {
                spawnpos.x += ad;
                rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                spawnpos.x -= ad;
                rotation = Quaternion.Euler(0, 0, 0);
            }
            Instantiate(slashPrefab, spawnpos, rotation);
            canAttack = false;
            StartCoroutine(attackCooldownRoutine());
        }
     }

     void die()
    {
        mobInfoScript.spawnCoin(Random.Range(1, 4));
        mobInfoScript.spawnHealthItem();
        animator.SetBool("isDead", true);
        alive = false;
        rb.freezeRotation = false;
        gameObject.tag = "Untagged"; 
        Instantiate(corpse, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    IEnumerator attackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}

