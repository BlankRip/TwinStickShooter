using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    //Variables related to enemy
    [SerializeField] int speed;
    [SerializeField] string tagThatDamageEnemy;
    [SerializeField] Slider healthBar;
    Rigidbody enemyRigidbody;
    int enemyHealth = 70;
    int phyDamageIDo = 5;                    // damage he inflicts on meele
    int pointsIGive = 14;                   // points player recieves on killing this enemy
    float gapBtwPhyDamage = 1.5f;                   // gap between meele attacks;
    bool playertookDamage = false;          // Check if player has taken physical damage

    // Variables related to bullets
    [SerializeField] GameObject bullet;
    float bulletSpawnGap1 = 4.4f;            // Gap between bullet spawns
    float bulletSpawnGap2 = 4.7f;            // Gap between bullet spawns
    float bulletSpawnGap3 = 5;               // Gap between bullet spawns

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

    
    void Update()
    {
        //To make enmey face the player at all times
        transform.LookAt(new Vector3(playertransform.position.x, transform.position.y, playertransform.position.z));

        //To make enemy shoot bullets
        bulletSpawnGap1 -= Time.deltaTime;
        bulletSpawnGap2 -= Time.deltaTime;
        bulletSpawnGap3 -= Time.deltaTime;


        if (bulletSpawnGap1 <= 0)
        {
            manager.audioSource.PlayOneShot(manager.enemyBulletShot);
            Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawnGap1 = 4.4f;
        }
        if (bulletSpawnGap2 <= 0)
        {
            manager.audioSource.PlayOneShot(manager.enemyBulletShot);
            Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawnGap2 = 4.7f;
        }
        if (bulletSpawnGap3 <= 0)
        {
            manager.audioSource.PlayOneShot(manager.enemyBulletShot);
            Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawnGap3 = 5;
            bulletSpawnGap2 = 4.7f;
            bulletSpawnGap1 = 4.4f;
        }

        //Setting the gap berween meele attacks
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
            //Reduce enemy health on collide
            if(enemyHealth > 10)
            {
                enemyHealth = enemyHealth - 10;
                healthBar.value = enemyHealth;
                Destroy(other.gameObject);
            }
            //Destroying enemy
            else if(enemyHealth <= 10)
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
