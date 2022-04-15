using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public GameObject[] spawnPoints;

    [SerializeField] private GameObject gameOverText;

    private float startDelay = 2;
    private float spawnInterval = 1;


    void Start()
    {
        StartSpawning();
    }
    
    void SpawnRandomZombie()
    {
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        int randomPos = Random.Range(0, spawnPoints.Length);

        Instantiate(zombiePrefabs[zombieIndex], spawnPoints[randomPos].transform.position, spawnPoints[randomPos].transform.rotation);
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnRandomZombie", startDelay, spawnInterval);
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnRandomZombie");
    }

    public void GameOver()
    {
        StopSpawning();
        gameOverText.SetActive(true);        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
