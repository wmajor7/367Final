using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 10f;
    private Vector2 destination;
    private Vector2 forward;
    public GameObject player;
    public bool returning;
    public CharacterController myChar;
    private float fireX;
    private float fireY;

    // Start is called before the first frame update
    void Start()
    {
        /*rb = GetComponent<Rigidbody2D>();
        myChar = player.GetComponent<CharacterController>();*/
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        myChar = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        destination = player.transform.position;
        Debug.Log(player.transform.position);

        if (myChar.facing == "up")
        {
            fireX = 0;
            fireY = 1;
        }
        else if (myChar.facing == "left")
        {
            fireX = -1;
            fireY = 0;
        }
        else if (myChar.facing == "down")
        {
            fireX = 0;
            fireY = -1;
        }
        else if (myChar.facing == "right")
        {
            fireX = 1;
            fireY = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (returning)
        {
            //transform.rotation = Quaternion.LookRotation(myChar.transform.position);
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
            rb.MovePosition(newPosition);
        }
        else if (!returning)
        {
            forward = new Vector2(fireX, fireY);
            rb.MovePosition(rb.position + forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        returning = true;
        //rb.velocity = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
    }
}