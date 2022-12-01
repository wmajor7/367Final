using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject hitBox;
    public GameObject SFXMngr;
    public GameObject birdProj;
    public GameObject launchOri;

    public float runSpeed = 5.0f;

    public int health = 12;
    public int maxHealth = 12;
    public HealthBar healthBar;
    public birdController bController;

    public bool canMove;
    public bool isAttacking;
    public string facing;

    public AudioSource charSFX;
    public AudioClip hit;
    public AudioClip swing;
    public AudioClip death;
    public AudioClip fall;
    public AudioClip pickup;
    public AudioClip hurt;
    public AudioClip healthPickup;

    public string sfx;

    public bool hasBird;
    public float launchVelY = 700f;
    public bool birdOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        hitBox.SetActive(false);
        charSFX = SFXMngr.GetComponent<AudioSource>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        facing = "up";
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") == -1.0f)
        {
            facing = "left";
        }
        else if (Input.GetAxisRaw("Horizontal") == 1.0f)
        {
            facing = "right";
        }
        else if (Input.GetAxisRaw("Vertical") == -1.0f)
        {
            facing = "down";
        }
        else if (Input.GetAxisRaw("Vertical") == 1.0f)
        {
            facing = "up";
        }

        if (facing == "up")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (facing == "left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (facing == "down")
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (facing == "right")
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            sfx = "Swing";
            PlaySFX();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
        }

        if(isAttacking)
        {
            hitBox.SetActive(true);
        }
        else
        {
            hitBox.SetActive(false);
        }


        if (health <= 0)
        {
            sfx = "Death";
            PlaySFX();
            Reload();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
            healthBar.SetHealth(health);
        }

        if (hasBird == true && birdOnScreen == false && Input.GetKeyDown(KeyCode.F))
        {
            GameObject bird = Instantiate(birdProj, launchOri.transform.position, launchOri.transform.rotation);
            //bird.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, launchVelY));
            bController = bird.GetComponent<birdController>();
            birdOnScreen = true;
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        }
    }

    public void PlaySFX()
    {
        if (sfx == "Hit")
        {
            charSFX.clip = hit;
        }
        else if (sfx == "Swing")
        {
            charSFX.clip = swing;
        }
        else if (sfx == "Death")
        {
            charSFX.clip = death;
        }
        else if (sfx == "Fall")
        {
            charSFX.clip = fall;
        }
        else if (sfx == "Pickup")
        {
            charSFX.clip = pickup;
        }
        else if (sfx == "Hurt")
        {
            charSFX.clip = hurt;
        }
        else if (sfx == "HealthPickup")
        {
            charSFX.clip = healthPickup;
        }

        charSFX.Play();
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void birdReset()
    {
        birdOnScreen = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Companion")
        {
            birdReset();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "HealthDrop")
        {
            health += 2;
            healthBar.SetHealth(health);
            sfx = "HealthPickup";
            PlaySFX();
            Destroy(other.gameObject);
        }
    }
}
