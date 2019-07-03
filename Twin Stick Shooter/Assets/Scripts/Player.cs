using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject playerProjectile;             // The bullet for player
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] GameObject gun3;
    [SerializeField] float speed;                   
    GameObject barrelObject;                                  // Barrel of the gun
    GameManager manager;                                      // game manager

    [Header("Bullet spawn points for gun 3")]
    [SerializeField] GameObject gun3BulletPositiveAngle;
    [SerializeField] GameObject gun3BulletNegitiveAngle;
    float bulletSpawnGap = 4.4f;

    //Variables required to make camera look at mouse pointer
    Camera gameCamera;
    float raylength;
    Vector3 playerLookAt;

    //Variables used to make movement using keyboard
    Vector3 keyboardInput;

    //Variables used to make movement using controller
    [HideInInspector] public bool usingController;                  // if keyboard input or controller input
    Rigidbody playerRigidbody;
    Vector3 controllerInput;
    Vector3 moveVelocity;


    // Assigning valuse to components on start
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        barrelObject = GameObject.Find("BulletSpawn");
        manager = FindObjectOfType<GameManager>();
        gameCamera = FindObjectOfType<Camera>();
    }


    // Update is called once per frame
    void Update()
    {

        if (!usingController)
        {
            //To move the player
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
            //keyboardInput = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
            //playerRigidbody.velocity = keyboardInput * speed;



            //Switching weapons
            if (Input.GetKey(KeyCode.Alpha1))
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                gun3.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                gun2.SetActive(true);
                gun1.SetActive(false);
                gun3.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                gun3.SetActive(true);
                gun1.SetActive(false);
                gun2.SetActive(false);
            }

            //To shoot bullets
            if (manager.powerUpActive)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    }
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && gun1.activeSelf == true)         // firing from gun type 1
                {
                    //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                }
                if (Input.GetKey(KeyCode.Mouse0) && gun2.activeSelf == true)             // firing from gun type 2
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && gun2.activeSelf == true)
                    {
                        bulletSpawnGap = 0;
                    }
                    bulletSpawnGap -= Time.deltaTime;
                    if (bulletSpawnGap <= 0)
                    {
                        //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                        Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                        bulletSpawnGap = 0.5f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && gun3.activeSelf == true)        // firing from gun type 3
                {
                    //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                    Instantiate(playerProjectile, barrelObject.transform.position, gun3BulletNegitiveAngle.transform.rotation);
                    Instantiate(playerProjectile, barrelObject.transform.position, gun3BulletPositiveAngle.transform.rotation);
                }

            }
            

            //To make camera look at mouse pointer
            Ray rayFromcam = gameCamera.ScreenPointToRay(Input.mousePosition);
            Plane  groundPlain = new Plane(Vector3.up, Vector3.zero);
            if(groundPlain.Raycast(rayFromcam, out raylength))
            {
                playerLookAt = rayFromcam.GetPoint(raylength);
                transform.LookAt(new Vector3(playerLookAt.x,transform.position.y, playerLookAt.z));
            }
        }

        if(usingController)
        {
            //To move player
            controllerInput = new Vector3(Input.GetAxisRaw("LHorizontal"), 0, Input.GetAxisRaw("LVertical"));
            moveVelocity = controllerInput * speed;

            //Switching weapons
            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                gun3.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Joystick1Button1))
            {
                gun2.SetActive(true);
                gun1.SetActive(false);
                gun3.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Joystick1Button3))
            {
                gun3.SetActive(true);
                gun1.SetActive(false);
                gun2.SetActive(false);
            }

            //To shoot bullets
            if (manager.powerUpActive)
            {
                if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button2))
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    }
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && gun1.activeSelf == true)   // firing for gun type 1
                {
                    //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                }
                if ((Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button2)) && gun2.activeSelf == true)          // firing fro gun type 2
                {
                    if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && gun2.activeSelf == true)
                    {
                        bulletSpawnGap = 0;
                    }
                    bulletSpawnGap -= Time.deltaTime;
                    if (bulletSpawnGap <= 0)
                    {
                        //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                        Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                        bulletSpawnGap = 0.5f;
                    }
                }
                if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && gun3.activeSelf == true)   // firing fro gun type 3
                {
                    //manager.audioSource.PlayOneShot(manager.playerBulletShot);
                    Instantiate(playerProjectile, barrelObject.transform.position, barrelObject.transform.rotation);
                    Instantiate(playerProjectile, barrelObject.transform.position, gun3BulletNegitiveAngle.transform.rotation);
                    Instantiate(playerProjectile, barrelObject.transform.position, gun3BulletPositiveAngle.transform.rotation);
                }
            }
            

            //To rotate with right stick
            Vector3 playerLookDirection = Vector3.forward * Input.GetAxisRaw("RVertical") + Vector3.right * Input.GetAxisRaw("RHorizontal");
            if(playerLookDirection.sqrMagnitude > 0)                       //This is to check if there is anymovement in the right stick if so the value will not be 0
            {
                transform.rotation = Quaternion.LookRotation(playerLookDirection);
            }

        }
    }

    //Using rigidBody and physiics to move when uisng controller
    private void FixedUpdate()
    {
        if(usingController)
        {
            playerRigidbody.velocity = moveVelocity;
        }
    }
}
