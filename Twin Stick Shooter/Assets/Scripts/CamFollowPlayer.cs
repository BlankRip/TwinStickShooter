using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    GameObject player;

    //Valuses of the postion of the camera needed to be added to the player postion to begun on start
    [SerializeField] Vector3 camOffSet;

    // Assigning valuse to components on start
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Updating the positon of camera to follow the player at all times
    void LateUpdate()
    {
        Vector3 requiredSpot = player.transform.position + camOffSet;
        transform.position = requiredSpot;

        transform.LookAt(player.transform);
    }
}
