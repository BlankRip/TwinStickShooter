using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyTikTakPower : MonoBehaviour
{
    // Variables related to this powerUp
    float powerUpTimer = 2;

    // Manager
    GameManager manager;

    // Assigning valuse to components on start
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }


    // On touch adding health to the player and destroying the object
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(manager.playerHealth < 130)
            {
                manager.playerHealth = manager.playerHealth + 20;
                manager.playerHealthBar.value = manager.playerHealth;
                Destroy(gameObject);
            }
            else if(manager.playerHealth >= 130)
            {
                manager.playerHealth = 150;
                manager.playerHealthBar.value = manager.playerHealth;
                Destroy(gameObject);
            }
        }
    }
}
