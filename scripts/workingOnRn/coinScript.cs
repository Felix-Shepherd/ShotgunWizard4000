using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class coinScript : MonoBehaviour

{

    [SerializeField] private AudioClip coinSound;
    public int coinValue = 1;

    float magnetRange = 5;
    private GameObject player;
    private float distance;
    private float speed = 2f;
    private Rigidbody2D rb;
    private bool hasJumpped = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = (player.transform.position - transform.position).magnitude;

        if (distance < magnetRange)
        {
            Jump();
            rb.AddForce((player.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
        }

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData playerData = collision.GetComponent<PlayerData>();
            playerData.addCoins(coinValue);
            Destroy(gameObject);
            playerData.updateCoinUI();
            soundManagerScript.instance.playSoundClip(coinSound, this.transform, 1f);
        }
    }

    private void Jump()
    {
        if (hasJumpped) return;
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        hasJumpped = true;
    }



}
