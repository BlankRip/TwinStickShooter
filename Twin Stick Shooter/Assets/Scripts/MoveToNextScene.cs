using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    // Loads the next scene based on bulid index
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads the menu scene
    public void backToMenu()
    {
        Destroy(audioSource);
        SceneManager.LoadScene(0);
    }
}
