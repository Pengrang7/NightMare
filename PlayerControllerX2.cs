using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX2 : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 800;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    public ParticleSystem smokeParticle;
    public ParticleSystem endParticle;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip Firecrackersound;

    public float jumpForce;
    private Animator playerAnim;
    public bool isOnGround=true;
    public bool gameOver=false;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        Destroy(GameObject.FindWithTag("Powerup"), 2);
        isOnGround=true;
    }


    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        //smokeParticle.transform.position = focalPoint.transform.position;

        if (Input.GetKey(KeyCode.Q))
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime*2);
            smokeParticle.Play();
            smokeParticle.transform.position = focalPoint.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Space)&&isOnGround&&!gameOver)
        {
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            isOnGround=false;
            //playerAnim.SetTrigger("Jump_trig");
            //dirtParticle.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

    }

    void AddForcePlayer(float speed)
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime*5);
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
        }
    }

    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }


    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Lose");
        }
        if (other.gameObject.tag == "Enemy2")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Lose");
        }
        if (other.gameObject.tag == "ground")
        {
            isOnGround = true;    //Ground에 닿으면 isGround는 true
        }
        if (other.gameObject.tag == "End")
        {
            Destroy(gameObject);
            //endParticle.Play();
            //playerAudio.PlayOneShot(Firecrackersound, 1.0f);
            SceneManager.LoadScene("Win");
        }
        if (other.gameObject.tag == "Next")
        {
            Destroy(gameObject);
            //endParticle.Play();
            //playerAudio.PlayOneShot(Firecrackersound, 1.0f);
            SceneManager.LoadScene("final");
        }
    }



}