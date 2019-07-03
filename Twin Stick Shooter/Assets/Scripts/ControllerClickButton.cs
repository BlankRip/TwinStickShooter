using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerClickButton : MonoBehaviour
{
    MoveToNextScene next;

    // Assigning valuse to components on start
    void Start()
    {
        next = FindObjectOfType<MoveToNextScene>();
    }

    //Using button press on the controller to move to next scene when on menu screens
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0)) next.nextScene();
    }
}
