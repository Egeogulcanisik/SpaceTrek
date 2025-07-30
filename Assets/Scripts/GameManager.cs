using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
AudioSource audio = GetComponent<AudioSource>();
if (backgroundMusic != null)
{
    audio.clip = backgroundMusic;
    audio.loop = true;
    audio.Play();
}
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("MainMenuScene");
    }
}

   
