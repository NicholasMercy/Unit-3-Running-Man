using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 10;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver;
    public bool fastMode = false;
    public bool introOn;

    public int counterJump;
    public int noJump;
    private int score;

    private float doubleSpeedAnim =2;
    private float speedAnim = 1;

    private float timer;

    public Transform startingPoint;
    public float lerpSpeed;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        counterJump = 0;
        score = 0;
        isOnGround = true;

        gameOver = true;
        StartCoroutine(PlayIntro());

    }

    // Update is called once per frame
    void Update()
    {       
        if(!gameOver && Time.time - timer >= 1f)
        {          
            Debug.Log(score);
            if(fastMode)
            {
                score +=2;
                timer = Time.time;
            }
            else
            {
                score++;
                timer = Time.time;
            }
        }
        //jump on ground and double jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            Jump();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && counterJump < noJump)
        {
            Jump();
        }
        else if(counterJump == noJump && isOnGround)
        {
            counterJump = 0;
        }

        if(Input.GetKey(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            fastMode = true;
            playerAnimator.speed = doubleSpeedAnim;
        }
        else if(!introOn)
        {
            playerAnimator.speed = speedAnim;
            fastMode = false;
        }
       
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {            
            isOnGround = true;
            dirtParticle.Play();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(crashSound);
            gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            Debug.Log("Game Over");
        }
    }

    void Jump()
    {
        counterJump++;
        playerAnimator.SetTrigger("Jump_trig");
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        dirtParticle.Stop();
        playerAudioSource.PlayOneShot(jumpSound);
        isOnGround = false;
        //Debug.Log(counterJump);
    }

    IEnumerator PlayIntro()
    {
        introOn = true;
        Vector3 endPos = transform.position;
        Vector3 startPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerAnimator.speed = 0.5f;
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        playerAnimator.speed = 0.5f;
        gameOver = false;
        introOn = false;
    }

}
