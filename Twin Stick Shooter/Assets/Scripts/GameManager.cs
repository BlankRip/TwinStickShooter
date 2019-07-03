﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UI elements
    public Slider playerHealthBar;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverScore;
    [SerializeField] GameObject DeadScreen;                       //Game over screen

    //Audio related
    [HideInInspector] public AudioSource audioSource;
    public AudioClip playerBulletShot;
    public AudioClip enemyBulletShot;
    public AudioClip enemyPhyAttack;
    public AudioClip playerDmgTaken;

    // Other
    MoveToNextScene backToMenu;
    public bool powerUpActive = false;    // if power up has been picked up and is active
    public bool gameOver = false;         // if player died or not
    public int playerHealth = 150;        // player health
    public int score = 0;                 // player score

    // Assigning values
    void Start()
    {
        playerHealthBar.value = playerHealth;
        backToMenu = FindObjectOfType<MoveToNextScene>();
        audioSource = FindObjectOfType<AudioSource>();
        Time.timeScale = 1;
    }

    // Update player score and health, and checks if player is dead
    void Update()
    {
        scoreText.text = "" + score;

        if (playerHealth <= 0) gameOver = true;

        if(gameOver == true)
        {
            gameOverScore.text = "" + score;
            DeadScreen.SetActive(true);
            if(Input.GetKey(KeyCode.Joystick1Button0)) backToMenu.backToMenu();
            Time.timeScale = 0;
        }
    }
}
