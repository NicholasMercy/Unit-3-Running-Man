using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    private int obstacleChoice;
    private Vector3 spawnPos = new Vector3(35,0,0);
    private float startDelay = 2f;
    private float repeatingRate = 2f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatingRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false)
        {
            obstacleChoice = UnityEngine.Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleChoice], spawnPos, obstaclePrefab[obstacleChoice].transform.rotation);
        }
        
    }
}
