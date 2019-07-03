using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
   
    // Loads the next scene based on bulid index
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads the menu scene
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
