using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLock : MonoBehaviour
{
    public GameObject player;
    public CharacterController myChar;

    // Start is called before the first frame update
    void Start()
    {
        myChar = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && myChar.keysHeld > 0)
        {
            myChar.keysHeld -= 1;
            Destroy(this.gameObject);
        }
    }
}
