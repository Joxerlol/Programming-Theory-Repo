using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;
    private float xRange = 17.5f;
    private float zbotRange = -7.5f;
    private float ztopRange = 11.5f;

    [SerializeField] private LayerMask groundMask;

    private Camera mainCamera;
    public List<GameObject> projectilePrefab;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Move();
        GameBorders();
        Aim();
        Shoot();
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, verticalInput * speed * Time.deltaTime);
    }

    private void GameBorders()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zbotRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zbotRange);
        }

        if (transform.position.z > ztopRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ztopRange);
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectilePrefab[0], transform.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(projectilePrefab[1], transform.position, transform.rotation);
        }
    }

    
}
