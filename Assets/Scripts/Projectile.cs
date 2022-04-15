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
