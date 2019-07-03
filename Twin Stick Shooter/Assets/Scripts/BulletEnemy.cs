using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    //Variables
    [SerializeField] float speed;
    int damageIDo = 15;                              // Amount of damage it inflicts on player
    GameManager manager;
    Rigidbody bulletRB;


    // Assigning valuse to components on start
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        bulletRB = GetComponent<Rigidbody>();
    }

    //Moving the bullet forward
    void FixedUpdate()
    {
        bulletRB.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //To destroy bullet when hits objects
        if(other.tag == "Environment")
        {
            Destroy(gameObject);
        }

        // Damaging the player when collided
        if(other.tag == "Player")
        {
            if(manager.playerHealth > damageIDo)
            {
                manager.audioSource.PlayOneShot(manager.playerDmgTaken);
                manager.playerHealth = manager.playerHealth - damageIDo;
                manager.playerHealthBar.value = manager.playerHealth;
                Destroy(gameObject);
            }
            else if(manager.playerHealth <= damageIDo)
            {
                manager.audioSource.PlayOneShot(manager.playerDmgTaken);
                manager.playerHealth = manager.playerHealth - damageIDo;
                manager.playerHealthBar.value = manager.playerHealth;
                Destroy(GameObject.Find("Player"));
                Destroy(gameObject);
            }
        }
    }
}
