using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTikTakPower : MonoBehaviour
{
    // Variables related to this powerUp
    float powerUpTimer = 2;
    [SerializeField] MeshRenderer objectMesh;
    [SerializeField] SphereCollider objectCollider;
    bool tookThis = false;

    // Manager
    GameManager manager;

    // Assigning valuse to components on start
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Hides the mesh and collider on touch and activates the power up
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            manager.powerUpActive = true;
            objectMesh.enabled = false;
            objectCollider.enabled = false;
            tookThis = true;
        }
    }

    // Checkes if power up is active and destroys after few seconds
    void Update()
    {
        if (manager.powerUpActive && tookThis == true)
        {
            if (powerUpTimer > 0)
            {
                powerUpTimer -= Time.deltaTime;
            }
            else if (powerUpTimer <= 0 && tookThis == true)
            {
                manager.powerUpActive = false;
                powerUpTimer = 2;
                Destroy(gameObject);
            }
        }
    }
}
