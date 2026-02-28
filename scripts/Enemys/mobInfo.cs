using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class mobInfo : MonoBehaviour
{


    public int maxHealth;
    public int currentHealth;
    public int mobNr;

    public GameObject healthItem;
    private float dropRate = 0.3f;

    public GameObject coin;

    [SerializeField] AudioClip damagedSound;

    void Start()
    {
        currentHealth = maxHealth;
        coin = globalItemHolder.instance.coin;
        healthItem = globalItemHolder.instance.healthItem;
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        soundManagerScript.instance.playSoundClip(damagedSound, this.transform, 1f);
    }
    

    public void spawnCoin(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            rb.linearVelocityY = 10f;
            
        }
    }

    public void spawnHealthItem()

    {
        if (Random.value < dropRate)
        {
            Instantiate(healthItem, transform.position, Quaternion.identity);
        }
    }

}
