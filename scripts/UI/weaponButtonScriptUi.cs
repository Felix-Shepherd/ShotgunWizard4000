using UnityEngine;

public class weaponButtonScriptUi : MonoBehaviour
{

    public GameObject canvas;
    public GameObject chest;
    private startchest chestScript;
    public GameObject wallToRemove;
    public GameObject shotgunPrefab;
    public GameObject waponmanager;
    private weaponManager weaponManagerScript;

    void Start()
    {
        chestScript = chest.GetComponent<startchest>();
        weaponManagerScript = waponmanager.GetComponent<weaponManager>();
    }

    public void generalAction()
    {
        toggelCanvas();
        openChest();
        removeWall();
    }
    private void toggelCanvas()
    {
        if (canvas.activeSelf)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    private void openChest()
    {
        chestScript.updateChest();
    }

    private void removeWall()
    {
        Destroy(wallToRemove);
    }


    public void shotgun()
    {
        chestScript.shotgunsapwn();
    }

}