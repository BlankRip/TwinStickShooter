using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables
    [SerializeField] float speed;
    Rigidbody bulletRB;

    // Assigning valuse to components on start
    private void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    //Moving the bullet forward
    void FixedUpdate()
    {
        bulletRB.velocity = transform.forward * speed;
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
