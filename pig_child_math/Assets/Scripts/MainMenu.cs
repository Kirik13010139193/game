using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip buttonClick;
    void Start()
    {
        
    }

    public void Play()
    {
        aud.clip = buttonClick;
        aud.Play();
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
        yield return null;
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }
}
