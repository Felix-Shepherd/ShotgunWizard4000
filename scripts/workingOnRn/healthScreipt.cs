using UnityEngine;

public class healthScreipt : MonoBehaviour
{
    private GameObject player;
    public int value = 1;
    private PlayerData playerData;
    public AudioClip healSound;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerData.increaseHealth(value);
            Destroy(gameObject);
            soundManagerScript.instance.playSoundClip(healSound, this.transform, 1f);
        }
    }

}
