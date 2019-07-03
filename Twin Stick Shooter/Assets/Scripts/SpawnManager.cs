using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header ("Input Enemy prefabs")]
    [SerializeField] GameObject enemyType1;
    [SerializeField] GameObject enemyType2;
    [SerializeField] GameObject enemyType3;

    [Header("Input powerUp prefabs")]
    [SerializeField] GameObject powerUpType1;
    [SerializeField] GameObject powerUpType2;

    [Header("Input Spawn Positions")]
    [SerializeField] Transform enemySpawn1;
    [SerializeField] Transform enemySpawn2;
    [SerializeField] Transform enemySpawn3;
    [SerializeField] Transform enemySpawn4;
    [SerializeField] Transform enemySpawn5;
    [SerializeField] Transform enemySpawn6;

    // Variables related to spawning
    [HideInInspector] public int spawnedNumber = 0;               // Number of enemis at the movement
    int enemyLimit = 20;                                          // Max number of enemies that can be on the game at an instance
    float gapBtwEnemySpawn = 3;                                   // Gap between enemy spawns
    int positionFromPlayerXZ = 7;                                 // The amount of distance aways fro the player on the X or Z axis
    float positionFromPlayerY = 0.75f;                            // Amount of distance away form the player on the Y axis
    int powerUpSpawnNumber = 0;                                   // Number of powerup at the movement
    int powerUpLimit = 7;                                         // Max number of powerUps that can be on the game at an instance
    float gapBtwPowerSpawn = 20;                                  // Gap between powerUP spawns

    int whichEnemy;                                               // Choosing which type of enemy to spawn
    int whereToSpawn;                                             // Choosing where to spawn the enemy
    int whichPower;                                               // Choosing which type of power up to spawn
    int whichSide;                                                // Choosing which side of the player to spawn

    GameManager manager;
    Player player;


    // Assigning valuse to components on start
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        // Spawning random enemies at random spawnpoints
        if(spawnedNumber < enemyLimit)                       // Check if numer of enmey on the screen is less than max limit
        {
            gapBtwEnemySpawn -= Time.deltaTime;
            whichEnemy = Random.Range(0, 3);
            whereToSpawn = Random.Range(0, 6);
            if (gapBtwEnemySpawn <= 0)
            {
                if(whereToSpawn == 0)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn1.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn1.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn1.position, Quaternion.identity);
                }
                else if (whereToSpawn == 1)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn2.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn2.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn2.position, Quaternion.identity);
                }
                else if (whereToSpawn == 2)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn3.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn3.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn3.position, Quaternion.identity);
                }
                else if(whereToSpawn == 3)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn4.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn4.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn4.position, Quaternion.identity);
                }
                else if(whereToSpawn == 4)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn5.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn5.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn5.position, Quaternion.identity);
                }
                else if(whereToSpawn == 5)
                {
                    if (whichEnemy == 0) Instantiate(enemyType1, enemySpawn6.position, Quaternion.identity);
                    if (whichEnemy == 1) Instantiate(enemyType2, enemySpawn6.position, Quaternion.identity);
                    if (whichEnemy == 2) Instantiate(enemyType3, enemySpawn6.position, Quaternion.identity);
                }

                spawnedNumber++;
                if (manager.score > 2000)
                {
                    gapBtwEnemySpawn = 0.75f;
                    enemyLimit = 45;
                }
                else if (manager.score > 1500)
                {
                    gapBtwEnemySpawn = 1.5f;
                    enemyLimit = 40;
                }
                else if (manager.score > 1000)
                {
                    gapBtwEnemySpawn = 2;
                    enemyLimit = 30;
                }
                else if (manager.score > 750)
                {
                    gapBtwEnemySpawn = 2;
                    enemyLimit = 25;
                }
                else if (manager.score > 500)
                {
                    gapBtwEnemySpawn = 2.5f;
                    enemyLimit = 20;
                }
                else
                {
                    gapBtwEnemySpawn = 3;
                }
            }
        }

        // Spawning random powerUps at random sides of the player
        if(powerUpSpawnNumber < powerUpLimit)
        {
            gapBtwPowerSpawn -= Time.deltaTime;
            whichPower = Random.Range(0, 2);
            whichSide = Random.Range(0, 4);
            if (gapBtwPowerSpawn <= 0)
            {
                if (whichSide == 0)
                {
                    if (whichPower == 0) Instantiate(powerUpType1, new Vector3(player.transform.position.x, player.transform.position.y + positionFromPlayerY, player.transform.position.z + positionFromPlayerXZ), Quaternion.identity);
                    if (whichPower == 1) Instantiate(powerUpType2, new Vector3(player.transform.position.x, player.transform.position.y + positionFromPlayerY, player.transform.position.z + positionFromPlayerXZ), Quaternion.identity);
                }
                else if (whichSide == 1)
                {
                    if (whichPower == 0) Instantiate(powerUpType1, new Vector3(player.transform.position.x + positionFromPlayerXZ, player.transform.position.y + positionFromPlayerY, player.transform.position.z), Quaternion.identity);
                    if (whichPower == 1) Instantiate(powerUpType2, new Vector3(player.transform.position.x + positionFromPlayerXZ, player.transform.position.y + positionFromPlayerY, player.transform.position.z), Quaternion.identity);
                }
                else if (whichSide == 2)
                {
                    if (whichPower == 0) Instantiate(powerUpType1, new Vector3(player.transform.position.x, player.transform.position.y + positionFromPlayerY, player.transform.position.z - positionFromPlayerXZ), Quaternion.identity);
                    if (whichPower == 1) Instantiate(powerUpType2, new Vector3(player.transform.position.x, player.transform.position.y + positionFromPlayerY, player.transform.position.z - positionFromPlayerXZ), Quaternion.identity);
                }
                else if (whichSide == 3)
                {
                    if (whichPower == 0) Instantiate(powerUpType1, new Vector3(player.transform.position.x - positionFromPlayerXZ, player.transform.position.y + positionFromPlayerY, player.transform.position.z), Quaternion.identity);
                    if (whichPower == 1) Instantiate(powerUpType2, new Vector3(player.transform.position.x - positionFromPlayerXZ, player.transform.position.y + positionFromPlayerY, player.transform.position.z), Quaternion.identity);
                }

                powerUpSpawnNumber++;
                gapBtwPowerSpawn = 20;
            }
        }
        
    }
}
