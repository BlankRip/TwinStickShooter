using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    GameObject player;
    [SerializeField] float smoothspeed = 0.125f;

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
        Vector3 smoothPosition = Vector3.Lerp(transform.position, requiredSpot, smoothspeed);
        transform.position = smoothPosition;

        transform.LookAt(player.transform);
    }
}
