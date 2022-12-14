using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonTransition : MonoBehaviour
{
    public GameObject iceDoor;
    public GameObject fireDoor;
    public GameObject OverworldDoor;
    private string dungeon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "IceDungeon")
        {
            SceneManager.LoadScene("Overworld2");
        }
        else if (other.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "Overworld")
        {
            SceneManager.LoadScene("IceDungeon");
        } 
        else if (other.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "Overworld2")
        {
            SceneManager.LoadScene("FireDungeon");
        }
    }
}
