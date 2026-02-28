using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartManager : MonoBehaviour
{

    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    
        
    public PlayerData playerData;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
    }

    
 
    public void UpdateHealth()
    {
        for (int i = 0; i < playerData.maxHealth; i++)
        {
            if (i < playerData.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        } 
    }
}
