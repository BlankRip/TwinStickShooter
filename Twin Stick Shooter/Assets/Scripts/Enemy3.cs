using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3 : MonoBehaviour
{
    //Variables related to enemy
    [SerializeField] int speed;
    [SerializeField] string tagThatDamageEnemy;
    [SerializeField] Slider healthSlider;
    Rigidbody enemyRigidbody;
    int enemyHealth = 140;
    int phyDamageIDo = 5;                      // damage he inflicts on meele
    int pointsIGive = 12;                     // points player recieves on killing this enemy
    float gapBtwPhyDamage = 1.5f;                 // gap between meele attacks;
    bool playertookDamage = false;            // Check if player has taken physical damage

    // Managers and other objects used in this script
    GameManager manager;
    SpawnManager enemyNumberTracker;
    Transform playertransform;

    //Assigning values initially
    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        playertransform = FindObjectOfType<Player>().transform;
        manager = FindObjectOfType<GameManager>();
        enemyNumberTracker = FindObjectOfType<SpawnManager>();
        healthSlider.value = enemyHealth;
    }

    
    void Update()
    {
        //To make enmey face the player at all times
        transform.LookAt(new Vector3(playertransform.position.x, transform.position.y, playertransform.position.z));

        //Setting the gap berween meele attacks
        if (playertookDamage == true)
        {
            if(gapBtwPhyDamage > 0) gapBtwPhyDamage -= Time.deltaTime;
            if(gapBtwPhyDamage <= 0)
            {
                playertookDamage = false;
                gapBtwPhyDamage = 1.5f;
            }
        }
    }

    //Moving the enemy forward
    private void FixedUpdate()
    {
        enemyRigidbody.velocity = (transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagThatDamageEnemy)
        {
            //Reduce enemy health on collide
            if(enemyHealth > 10)
            {
                enemyHealth = enemyHealth - 10;
                healthSlider.value = enemyHealth;
                Destroy(other.gameObject);
            }
            //Destroying enemy
            else if(enemyHealth <= 10)
            {
                Destroy(other.gameObject);
                healthSlider.value = enemyHealth;
                manager.score = manager.score + pointsIGive;
                enemyNumberTracker.spawnedNumber--;
                Destroy(gameObject);
            }
        }
    }

    // To make the player tage damage with meele attacks
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && playertookDamage == false)
        {
            print("i do");
            if (manager.playerHealth > phyDamageIDo)
            {
                manager.audioSource.PlayOneShot(manager.enemyPhyAttack);
                manager.playerHealth = manager.playerHealth - phyDamageIDo;
                manager.playerHealthBar.value = manager.playerHealth;
            }
            if (manager.playerHealth <= phyDamageIDo)
            {
                manager.audioSource.PlayOneShot(manager.enemyPhyAttack);
                manager.playerHealth = manager.playerHealth - phyDamageIDo;
                manager.playerHealthBar.value = manager.playerHealth;
                Destroy(GameObject.Find("Player"));
            }
            playertookDamage = true;
        }
    }

}
