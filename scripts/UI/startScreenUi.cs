using UnityEngine;

public class startScreenUi : MonoBehaviour
{

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomPrefabCreation");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Options Menu");
    }

}
