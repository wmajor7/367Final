using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLock : MonoBehaviour
{
    public GameObject player;
    public CharacterController myChar;
    public GameObject door;
    public GameObject door2;

    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
        door2.SetActive(false);
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
            door.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
