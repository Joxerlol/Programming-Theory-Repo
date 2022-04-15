using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 6.5f;

    private int damage = 10;
    private float damageTimer;
    protected GameObject player;
    private PlayerController playerCon;
    private AudioSource audioSource;    
  
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip attackSound;
    
    void Start()
    {        
        player = GameObject.Find("Player");
        playerCon = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Update()
    {
        ChasePlayer();
        DamageTimer();
        ClearTheScreen();
    }

    private void OnDestroy()
    {
        audioSource.PlayOneShot(deathSound, 0.3f);
    }

    private void ClearTheScreen()
    {
        if (!playerCon.isAlive)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ChasePlayer()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * speed); 
    }   
    
    public void DealDamage()
    {
        playerCon.Health -= damage;
    }

    private void DamageTimer()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            if (damageTimer <= 0)
            {
                damageTimer = 1;
                audioSource.PlayOneShot(attackSound, 0.3f);
                DealDamage();
            }
                        
        }
    }

}
