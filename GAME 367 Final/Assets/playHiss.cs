using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playHiss : MonoBehaviour
{
    public AudioSource hiss;
    public bool play;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("bool" + play);
        Debug.Log("Playing" + hiss.isPlaying);
    }

    public void PlayHiss()
    {
        if (play == true && hiss.isPlaying == false)
        {
            hiss.Play();
        }
    }    
}
