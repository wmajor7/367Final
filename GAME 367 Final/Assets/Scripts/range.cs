using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range : MonoBehaviour
{
    public Enemy my_Enemy;
    public CharacterController myChar;

    // Start is called before the first frame update
    void Start()
    {
        my_Enemy = GetComponentInParent<Enemy>();
        myChar = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            my_Enemy.atkMode = true;

            myChar.attackers += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            my_Enemy.atkMode = false;

            myChar.attackers -= 1;
        }
    }

}
