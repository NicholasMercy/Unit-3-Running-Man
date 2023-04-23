using System.Collections;
using System.Collections.Generic;
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
    public int counterJump;
    public int noJump;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        counterJump = 0;
        isOnGround = true;
        gameOver = false;   

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            
            counterJump++;            
            playerAnimator.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(jumpSound);
            isOnGround = false;
            //Debug.Log(counterJump);

        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && counterJump < noJump)
        {
            counterJump++;
            playerAnimator.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(jumpSound);
            isOnGround = false;
            Debug.Log(counterJump);

        }
        else if(counterJump == noJump && isOnGround)
        {
            counterJump = 0;
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
}
