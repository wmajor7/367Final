using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private Vector2 destination;
    private Vector2 forward;
    public GameObject player;
    public bool returning;
    public CharacterController myChar;
    private float fireX;
    private float fireY;

    public Sprite flippedSwitch;
    public GameObject locked;

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
        locked = GameObject.Find("Lock");
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
        if (other.gameObject.tag == "Switch")
        {
            other.gameObject.GetComponent<SpriteRenderer>().sprite = flippedSwitch;
            myChar.door.SetActive(true);
            Destroy(locked);
        }

        returning = true;
        //rb.velocity = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
        }
    }
}
