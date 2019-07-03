using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    // When attached to an object it is not destroyed when scenes are changed
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
