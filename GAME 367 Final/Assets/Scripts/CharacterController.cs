using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject hitBox;

    public float runSpeed = 5.0f;

    public int health = 12;

    public bool canMove;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
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
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        }
    }
}
