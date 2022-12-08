using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D krb;
    private Vector2 movement;
    public GameObject hitBox;
    public GameObject SFXMngr;
    public GameObject birdProj;
    public GameObject launchOri;
    public GameObject center;
    public GameObject lava;
    public GameObject snowball;

    public float runSpeed = 5.0f;

    public int health = 12;
    public int maxHealth = 12;
    public HealthBar healthBar;
    public birdController bController;

    public bool canMove;
    public bool isAttacking;
    public string facing;

    public AudioSource charSFX;
    public AudioSource footsteps;
    public AudioSource BGM;
    public AudioSource battle;
    public AudioSource key;
    public AudioSource doorOpen;
    public AudioClip hit;
    public AudioClip swing;
    public AudioClip death;
    public AudioClip fall;
    public AudioClip pickup;
    public AudioClip hurt;
    public AudioClip healthPickup;
    public AudioClip overworldBGM;
    public AudioClip iceBGM;
    public AudioClip battleBGM;
    public bool playing = false;
    public bool footstepSFX = false;
    public AudioMixer mixer;

    public string sfx;

    public bool hasBird;
    public float launchVelY = 700f;
    public bool birdOnScreen;

    public Animator mAnim;



    public int birdsKilled = 0;
    private Scene currentScene;
    public bool iceDungeon;

    public int keysHeld;

    public GameObject birdFriend;
    public bool cVis;

    public GameObject door;

    public bool inBattle;
    public bool battlePlaying;
    public bool BGMplaying;

    public int attackers;

    public bool hasSnowball;

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
        mAnim = GetComponent<Animator>();
        battlePlaying = false;
        BGMplaying = true;
        cVis = false;
        birdFriend.SetActive(false);
        doorOpen = GameObject.Find("DoorOpen").GetComponent<AudioSource>();
        snowball.SetActive(false);
    }

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        cVis = false;
        birdFriend.SetActive(false);

        snowball.SetActive(false);

        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "IceDungeon")
        {
            iceDungeon = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") == -1.0f)
        {
            facing = "left";

            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 1.0f)
        {
            facing = "right";

            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (Input.GetAxisRaw("Vertical") == -1.0f)
        {
            facing = "down";
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 1.0f)
        {
            facing = "up";
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 0.0f && Input.GetAxisRaw("Horizontal") == 0.0f)
        {
            footsteps.Stop();
        }

        if (facing == "up")
        {
            center.transform.rotation = Quaternion.Euler(0, 0, 0);
            mAnim.SetInteger("walk", 2);
        }
        else if (facing == "left")
        {
            center.transform.rotation = Quaternion.Euler(0, 0, 90);
            mAnim.SetInteger("walk", 3);
        }
        else if (facing == "down")
        {
            center.transform.rotation = Quaternion.Euler(0, 0, 180);
            mAnim.SetInteger("walk", 0);
        }
        else if (facing == "right")
        {
            center.transform.rotation = Quaternion.Euler(0, 0, 270);
            mAnim.SetInteger("walk", 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            mAnim.ResetTrigger("isAttacking");
            isAttacking = true;
            mAnim.SetTrigger("isAttacking");
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


        if (health <= 0 && !playing)
        {
            StartCoroutine(Wait());
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

        if (iceDungeon == true && birdsKilled == 4)
        {
            cVis = true;
        }

        if (cVis)
        {
            birdFriend.SetActive(true);
        }
        else if (!cVis)
        {
            birdFriend.SetActive(false);
        }    

        if (attackers > 0)
        {
            inBattle = true;
        }
        else if (attackers <= 0)
        {
            inBattle = false;
        }

        if (inBattle == true && BGM.isPlaying)
        {
            PlayBattle();
        }
        else if (!inBattle && battle.isPlaying)
        {
            PlayBGM();
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

    public void PlayBGM()
    {
        StartCoroutine(FadeAudioSource.StartFade(battle, 1f, 0f));
        if (battle.volume == 0)
        {
            battle.Pause();
        }

        BGM.volume = 0;
        BGM.Play();
        if (SceneManager.GetActiveScene().name == "IceDungeon")
        {
            StartCoroutine(FadeAudioSource.StartFade(BGM, 1f, 0.05f));
        }
        else if (SceneManager.GetActiveScene().name != "IceDungeon")
        {
            StartCoroutine(FadeAudioSource.StartFade(BGM, 1f, 1f));
        }
    }
    public void PlayBattle()
    {
        StartCoroutine(FadeAudioSource.StartFade(BGM, 1f, 0f));
        if(BGM.volume == 0)
        {
            BGM.Pause();
        }

        battle.volume = 0;
        battle.Play();
        StartCoroutine(FadeAudioSource.StartFade(battle, 1f, 20f));   
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
        else if (other.gameObject.tag == "HealthUpgrade")
        {
            maxHealth += 1;
            health = maxHealth;
            healthBar.SetHealth(health);
            sfx = "HealthPickup";
            PlaySFX();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Key")
        {
            keysHeld += 1;
            key.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Snowball")
        {
            hasSnowball = true;
            Destroy(other.gameObject);
        }    
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        krb = obj.GetComponent<Rigidbody2D>();
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            krb.AddForce(-direction * knockbackPower);
            yield return 0;
        }
    }

    public IEnumerator Wait()
    {
        playing = true;
        sfx = "Death";
        PlaySFX();
        yield return new WaitForSeconds(1);
        playing = false;
        Reload();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DirtDetector")
        {
            mixer.SetFloat("pitch", 0.34f);
            mixer.SetFloat("pitchShifter", 0.75f);
            mixer.SetFloat("FFT", 517f);
            mixer.SetFloat("Overlap", 4.6f);
        }
        else if (other.gameObject.tag == "GrassDetector")
        {
            mixer.SetFloat("pitch", 0.34f);
            mixer.SetFloat("pitchShifter", 1.45f);
            mixer.SetFloat("FFT", 517f);
            mixer.SetFloat("Overlap", 4.6f);
        }
        else if (other.gameObject.tag == "StoneDetector")
        {
            mixer.SetFloat("pitch", 0.34f);
            mixer.SetFloat("pitchShifter", 0.50f);
            mixer.SetFloat("FFT", 256f);
            mixer.SetFloat("Overlap", 1.00f);
        }
        else if (other.gameObject.tag == "IceDetector")
        {
            mixer.SetFloat("pitch", 0.34f);
            mixer.SetFloat("pitchShifter", 2.0f);
            mixer.SetFloat("FFT", 4096f);
            mixer.SetFloat("Overlap", 1.00f);
        }
        else if (other.gameObject.tag == "End" && hasSnowball == true)
        {
            canMove = false;
            snowball.SetActive(true);
            Camera.main.GetComponent<CameraMovement>().target = snowball.transform;
            lava.gameObject.GetComponent<playHiss>().play = true;
            lava.gameObject.GetComponent<playHiss>().PlayHiss();
            StartCoroutine(CoolLava());
        }
    }

    IEnumerator CoolLava()
    {
        yield return new WaitForSeconds(2f);
        Destroy(lava);
        Destroy(snowball);
        Camera.main.GetComponent<CameraMovement>().target = this.transform;
        canMove = true;
    }
}
