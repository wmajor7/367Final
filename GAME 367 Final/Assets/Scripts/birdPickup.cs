using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdPickup : MonoBehaviour
{
    public GameObject player;
    public CharacterController myChar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        myChar = player.GetComponent<CharacterController>();
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == player)
        {
            myChar.hasBird = true;
            Destroy(this.gameObject);
        }

    }
}
