using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    private float intialSpeed = 30f;
    private float speed;
    private float doubleSpeed = 60f;
    private PlayerController playerControllerScript;
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        speed = intialSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.fastMode == true)
        {
            speed = doubleSpeed;
        }
        else if (playerControllerScript.fastMode == false)
        {
            speed = intialSpeed;
        }

        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        
        
    }
}
