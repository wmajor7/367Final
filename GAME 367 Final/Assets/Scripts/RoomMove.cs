using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    public Vector3 iceChange;
    private CameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cam.minPos =  cam.minPos + cameraChange;
            cam.maxPos = cam.maxPos + cameraChange;
            other.transform.position = other.transform.position + playerChange;
            cam.iceTarg.position = cam.iceTarg.transform.position + iceChange;
        }
    }
}
