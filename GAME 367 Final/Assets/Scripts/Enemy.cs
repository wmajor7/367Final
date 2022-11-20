using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private CharacterController myChar;
    public GameObject player;

    public int health;

    public float moveSpeed;

    private int randNum;

    private bool waiting; 
    private bool doneMoving;
    public GameObject wallCheck1;
    public GameObject wallCheck2;
    public GameObject wallCheck3;
    public GameObject wallCheck4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0.0f, 0.0f);
        randNum = 0;
        doneMoving = false;
        myChar = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        //if the random number is zero, wait 2 seconds and assign a new random number
        if (randNum == 0)
        {
            StartCoroutine(WaitSeconds());
        }
        
        //if random number is within ranges, assign move direction
        if (movement.x == 0.0f && movement.y == 0.0f && randNum >= 1 && randNum <= 5)
        {
            movement.x = 1.0f;
            StartCoroutine(MoveSeconds());
        }
        else if (movement.x == 0.0f && movement.y == 0.0f && randNum >= 6 && randNum <= 10)
        {
            movement.x = -1.0f;
            StartCoroutine(MoveSeconds());
        }
        else if (movement.x == 0.0f && movement.y == 0.0f && randNum >= 11 && randNum <= 15)
        {
            movement.y = 1.0f;
            StartCoroutine(MoveSeconds());
        }
        else if (movement.x == 0.0f && movement.y == 0.0f && randNum >= 16 && randNum <= 20)
        {
            movement.y = -1.0f;
            StartCoroutine(MoveSeconds());
        }

        //when done moving, return randNum to 0 and stop movement
        if (doneMoving)
        {
            randNum = 0;
            movement.y = 0.0f;
            movement.x = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void rando()
    {
        randNum = Random.Range(1, 21);
    }

    IEnumerator WaitSeconds()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        rando();
        waiting = false;
    }
    IEnumerator MoveSeconds()
    {
        doneMoving = false;
        yield return new WaitForSeconds(2);
        doneMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            movement.x *= -1;
            movement.y *= -1;
        }
        else if (other.gameObject.tag == "Player")
        {
            myChar.health -= 1;
        }
        else if (other.gameObject.tag == "hitBox")
        {
            health -= 1;
        }
    }
}
