using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    //Variables
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    GameObject target;                           //The enemy to follow
    Rigidbody bulletRB;
    Vector3 bulletDirection;                     // direction in which it has to move to follow the target enemy

    // Assigning valuse to components on start
    private void Start()
    {
        player = GameObject.Find("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject temp;
        float shortestDistance = Mathf.Infinity;
        for (int i = 0; i < enemies.Length; i++)
        {
            temp = enemies[i].gameObject;
            float distance = Vector3.Distance(player.transform.position, temp.transform.position);

            //Checking if this enemy is closer to player that the time the bullet is shot
            if(distance < shortestDistance)
            {
                target = enemies[i].gameObject;
                shortestDistance = distance;
            }
        }
        bulletRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bulletDirection = (target.transform.position - player.transform.position).normalized;
    }

    //Moving the bullet forward
    void FixedUpdate()
    {
        bulletRB.velocity = bulletDirection * speed;
    }

    //Destroying the bullet if it hits anything
    private void OnTriggerEnter(Collider other)
    {
        //To destroy bullet when hits objects
        if(other.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
