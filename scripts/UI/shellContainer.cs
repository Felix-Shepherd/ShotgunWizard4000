using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class shellContainer : MonoBehaviour
{
    public List<Image> shells;
    public Sprite fullShell;
    public Sprite emptyShell;
    
    public GameObject reloadSymbol;
    private PlayerData playerData;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
        updateShells();
    }

    public void toggleReloadSymbol(bool show){
        reloadSymbol.SetActive(show);
    }
 
    public void updateShells()
    {
        for (int i = 0; i < playerData.maxShells; i++)
        {
            if (i < playerData.currentShells)
            {
                shells[i].sprite = fullShell;
            }
            else
            {
                shells[i].sprite = emptyShell;
            }
        } 
    }





}
