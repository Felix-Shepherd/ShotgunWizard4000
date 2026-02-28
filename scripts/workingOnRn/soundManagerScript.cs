using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    public static soundManagerScript instance;
    public AudioSource soundObject;

    public void Awake()
    {
     if (instance == null)
        {
            instance = this;
        }   
    }
    public void playSoundClip(AudioClip clip, Transform spawnTr, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTr.position, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
        float clipLeangth = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLeangth);
    }
}


