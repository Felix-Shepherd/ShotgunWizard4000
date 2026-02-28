using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmManager : MonoBehaviour
{
    public List<AudioClip> bgmClips;
    private AudioSource audioSource;
    public float pauseTime = 5f;
    private bool isPaused = false;

    public static bgmManager Instance; 

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayLoop());
    }

    IEnumerator PlayLoop()
    {
        while (true)
        {
            playRandomBgm(0.25f);
            yield return new WaitForSeconds(audioSource.clip.length);
            yield return new WaitForSeconds(pauseTime);
        }
    }

    public void playRandomBgm(float volume)
    {
        if (bgmClips == null || bgmClips.Count == 0) return;
        int randomIndex = Random.Range(0, bgmClips.Count);
        audioSource.clip = bgmClips[randomIndex];
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PauseAudio()
    {
        if (!isPaused)
        {
            audioSource.Pause();
            isPaused = true;
        }
    }

    public void ResumeAudio()
    {
        if (isPaused)
        {
            audioSource.UnPause();
            isPaused = false;
        }
    }
}
