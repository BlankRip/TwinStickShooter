using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyControlls : MonoBehaviour
{
    [SerializeField] Player thePlayer;

    void Update()
    {
        //Detecting if input is from keyboard and mouse
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Mouse0))
        {
            thePlayer.usingController = false;
        }

        //Detecting if controller is being used
        if(Input.GetAxisRaw("LHorizontal") != 0 || Input.GetAxisRaw("LVertical") != 0 || Input.GetAxisRaw("RHorizontal") != 0 || Input.GetAxisRaw("RVertical") != 0 || 
            Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button5))
        {
            thePlayer.usingController = true;
        }
        
    }
}
