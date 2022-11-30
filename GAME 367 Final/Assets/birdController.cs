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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myChar = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (returning)
        {
            destination = player.transform.position;
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
            rb.MovePosition(newPosition);
        }
        else if (!returning)
        {
            forward = new Vector2(0, 1);
            rb.MovePosition(rb.position + forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        returning = true;
        rb.velocity = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
    }
}
