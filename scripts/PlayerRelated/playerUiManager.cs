using UnityEngine;
using TMPro;


public class playerUiManager : MonoBehaviour
{

    private bool isActive;
    public GameObject pauseMenu;


    void Start()
    {
        toggleMenu();
        isActive = pauseMenu.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleMenu();
        }
    }
    public void toggleMenu()
{
    isActive = !pauseMenu.activeSelf;
    pauseMenu.SetActive(isActive);
    if (isActive)
    {
        Time.timeScale = 0f;
        bgmManager.Instance.PauseAudio();
        Debug.Log("on");
    }
    else
    {
        Time.timeScale = 1f;
        bgmManager.Instance.ResumeAudio();
        Debug.Log("off");
    }
}
//
}


