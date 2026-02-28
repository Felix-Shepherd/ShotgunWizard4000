using UnityEngine;

public class startchest : MonoBehaviour
{



    public GameObject shotgunPrefab;
    public GameObject startSelect;
    public Sprite chestClosed;
    public Sprite chestOpen;
    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;
    private Collider2D playerDetectorCol;
    public GameObject Uithing;
    public bool isTouching;
    public GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerDetectorCol = GetComponent<Collider2D>();
        updateChest();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collisoin booywa");
        if (collision.gameObject == player && isOpen == false)
        {
            Uithing.SetActive(true);
            isOpen = true;
        }
    }
    public void updateChest()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = chestOpen;
        }
        else
        {
            spriteRenderer.sprite = chestClosed;
        }
    }

        public void shotgunsapwn()
    {
        Vector3 playerPos = player.transform.position;
        Instantiate(shotgunPrefab, playerPos, Quaternion.identity);
    }




}
