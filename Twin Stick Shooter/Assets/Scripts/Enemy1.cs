using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    //Variables related to enemy
    [SerializeField] int speed;
    [SerializeField] string tagThatDamageEnemy;
    [SerializeField] Slider healthBar;
    Rigidbody enemyRigidbody;
    int enemyHealth = 100;
    int phyDamageIDo = 5;                         // damage he inflicts on meele
    int pointsIGive = 12;                        // points player recieves on killing this enemy 
    float gapBtwPhyDamage = 1.5f;                   // gap between meele attacks
    bool playertookDamage = false;               // Check if player has taken physical damage
    int damageTaken = 15;                        // Damage taken when whit by player bullet

    // Variables related to bullets
    [SerializeField] GameObject bullet;
    float spawnGap = 3;                          // Gap between bullet spawns
    float bulletSpawnGap;                    

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
        healthBar.value = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //To make enmey face the player at all times
        transform.LookAt(new Vector3(playertransform.position.x, transform.position.y, playertransform.position.z));

        //To make enemy shoot bullets
        bulletSpawnGap -= Time.deltaTime;
        if(bulletSpawnGap <= 0)
        {
            manager.audioSource.PlayOneShot(manager.enemyBulletShot);
            Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawnGap = spawnGap;
        }

        // Increasing bullet spawn speed with increase in player score
        if (manager.score > 2000)
        {
            spawnGap = 1.2f;
        }
        else if (manager.score > 1250)
        {
            spawnGap = 1.5f;
        }


        //Setting the gap berween meele attcks
        if (playertookDamage == true)
        {
            if (gapBtwPhyDamage > 0) gapBtwPhyDamage -= Time.deltaTime;
            if (gapBtwPhyDamage <= 0)
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
            //Reduce enemy health on collide with player bullets
            if(enemyHealth > damageTaken)
            {
                enemyHealth = enemyHealth - damageTaken;
                healthBar.value = enemyHealth;
                Destroy(other.gameObject);
            }
            //Destroying enemy
            else if(enemyHealth <= damageTaken)
            {
                Destroy(other.gameObject);
                healthBar.value = enemyHealth;
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
