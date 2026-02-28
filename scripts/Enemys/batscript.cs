using System.Collections;
using UnityEngine;

public class batscript : MonoBehaviour
{

    //hover 
    [SerializeField] private AudioClip throwSound;
    public float flapForce = 2.5f;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public float diff = 1f;
    private float maxS = 5f;
    private float minS = 0f;
    private GameObject player;
    public float flapInterval = 0.5f;
    public float hoverHeight = 5f;
    public float hoverRange = 5f;
    public bool isInRange = false;

    private Animator animator;
    private bool isDead = false;
    public GameObject FireBall;
    public float fireballSpeed = 10f;

    public float fireballCooldown = 5f;
    private bool isOnCooldown = false;
    private mobInfo _mobInfo;
    public GameObject explosion;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("flap", 0f, flapInterval);
        animator = GetComponentInChildren<Animator>();
        _mobInfo = GetComponent<mobInfo>();
    }

    void Update()
    {

        if (!isDead)
        {
            horiztontalMove();
            if (isInRange && !isOnCooldown)
            {
                shoot();
                StartCoroutine(FireballCooldown());
            }

        }
        
        if (_mobInfo.currentHealth <= 0 && !isDead)
        {
            CancelInvoke("flap");
            die();

        }

    }

    void flap()
    {
        if (rb.transform.position.y < player.transform.position.y + hoverHeight && flapForce < maxS)
        {

            flapForce += diff;
        }
        else if (rb.transform.position.y > player.transform.position.y + hoverHeight && flapForce > minS)
        {
            flapForce -= diff;
        }

        rb.linearVelocityY = flapForce;
    }


    private void shoot()
    {

        Vector2 aimDirection = (player.transform.position - transform.position).normalized;
        GameObject f = Instantiate(FireBall, transform.position, Quaternion.identity);
        Rigidbody2D frb = f.GetComponent<Rigidbody2D>();
        frb.linearVelocity = aimDirection * fireballSpeed;
        soundManagerScript.instance.playSoundClip(throwSound, this.transform, 1f);
    }


    private IEnumerator FireballCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(fireballCooldown);
        isOnCooldown = false;
    }

    void die()
    {
        animator.SetTrigger("isDead");
        isDead = true;
        _mobInfo.spawnCoin(Random.Range(1, 4));
        _mobInfo.spawnHealthItem();
    }


    void horiztontalMove()
    {

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 distance = player.transform.position - transform.position;

        if (distance.magnitude < hoverRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
        if (isInRange)
        {
            rb.linearVelocityX = direction.x * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && isDead)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
