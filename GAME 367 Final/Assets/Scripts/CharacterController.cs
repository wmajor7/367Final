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

    public float runSpeed = 5.0f;

    public int health = 12;
    public int maxHealth = 12;
    public HealthBar healthBar;

    public bool canMove;
    public bool isAttacking;

    public AudioSource charSFX;
    public AudioClip hit;
    public AudioClip swing;
    public AudioClip death;
    public AudioClip fall;
    public AudioClip pickup;
    public AudioClip hurt;

    public string sfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        hitBox.SetActive(false);
        charSFX = SFXMngr.GetComponent<AudioSource>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
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

        charSFX.Play();
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
