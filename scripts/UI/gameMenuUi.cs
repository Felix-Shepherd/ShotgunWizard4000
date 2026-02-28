using UnityEngine;

public class gameMenuUi : MonoBehaviour
{
    private playerUiManager pui;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pui = player.GetComponent<playerUiManager>();
    }

    public void resume()
    {
        pui.toggleMenu();  
    }

    public void settings()
    {
        
    }

    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("startScreen");
    }
    public void respawn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("startScreen");
    }

    
}
