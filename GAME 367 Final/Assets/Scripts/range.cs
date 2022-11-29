using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range : MonoBehaviour
{
    public Enemy my_Enemy;

    // Start is called before the first frame update
    void Start()
    {
        my_Enemy = GetComponentInParent<Enemy>();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            if (other.gameObject.tag == "Player")
            {
                my_Enemy.atkMode = false;
            }
    }

}
