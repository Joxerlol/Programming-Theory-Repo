using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 8.0f;
    private float xRange = 17.5f;
    private float zbotRange = -7.5f;
    private float ztopRange = 11.5f;
    private float projectileCooldown = 1.0f;
    public bool isAlive;
    // ENCAPSULATION
    private int health = 50;
    public int Health
    { 
        get { return health; }
        set { health = value; } 
    }


    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Text hpCounter;

    private GameManager gameManager;
    private Camera mainCamera;
    public List<GameObject> projectilePrefab;


    private void Start()
    {
        isAlive = true;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainCamera = Camera.main;
    }

    void Update()
    {              
        // ABSTRACTION
        Move();
        GameBorders();
        Aim();
        Shoot();
        Die();
        UpdateHP();
        WeaponCooldown();
    }

    private void WeaponCooldown()
    {
        if (projectileCooldown > 0)
        {
            projectileCooldown -= Time.deltaTime;
        }
    }

    private void Die()
    {
        if (health == 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }

        if (!isAlive)
        {
            gameManager.GameOver();
        }
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
                 

        if (Input.GetKeyDown(KeyCode.Mouse0) && projectileCooldown <= 0)
        {
            projectileCooldown = 0.7f;
            Instantiate(projectilePrefab[0], transform.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && projectileCooldown <= 0)
        {
            projectileCooldown = 0.8f;
            Instantiate(projectilePrefab[1], transform.position, transform.rotation);
        }
        
        
    }    

    private void UpdateHP()
    {
        hpCounter.text = ("HP: " + health + "/50");
    }





}
