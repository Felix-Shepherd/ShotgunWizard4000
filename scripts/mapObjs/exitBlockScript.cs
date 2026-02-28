using UnityEngine;

public class exitBlockScript : MonoBehaviour
{

    [SerializeField] private AudioClip doorSound;
    private float delay = 1f;
    private int count = 0;
    void Update()
    {
        if (delay > 0f)
        {
            delay -= 0.1f;
        }
        else
        {
         GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            if (enemies.Length == 0)
            {
                count += 1;
                if (count > 10)
                {
                    Destroy(gameObject);
                    soundManagerScript.instance.playSoundClip(doorSound, this.transform, 1f);
                }
                
            }     
        }
        
    }
}
