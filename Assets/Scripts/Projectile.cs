using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 30.0f;
    private float zBorder = 25.0f;
    private float xBorder = 25.0f;


    void Update()
    {
        Launch();
        DestroyOutOfBounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") == true)
        {
            Destroy(gameObject);            
        }  
        
        if (CompareTag("ZombieFood") && other.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (CompareTag("DogFood") && other.CompareTag("Dog"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    void Launch()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.z > zBorder)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -zBorder)
        {
            Destroy(gameObject);
        }

        if (transform.position.x > xBorder)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -xBorder)
        {
            Destroy(gameObject);
        }
    }
    
}
