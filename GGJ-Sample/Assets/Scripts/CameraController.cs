using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 5.0f;
    public Vector3 startPos;

    private void OnEnable(){
        AppEvents.OnBubbleClick.OnTrigger += SnapToBubble;
    }

    private void OnDisable(){
        AppEvents.OnBubbleClick.OnTrigger -= SnapToBubble;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= Screen.height && Input.mousePosition.y < Screen.height * 1.05))
        {
            transform.position += new Vector3(0, 1.0f, 0) * Speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= Screen.height *(1 - 0.975) && Input.mousePosition.y > -1.25))
        {
            transform.position -= new Vector3(0, 1.0f, 0) * Speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x <= Screen.width *(1 - 0.975) && Input.mousePosition.x > -1.25))
        {
            transform.position -= new Vector3(1.0f, 0, 0) * Speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= Screen.width && Input.mousePosition.x < Screen.width * 1.05))
        {
            transform.position += new Vector3(1.0f, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= Speed * Time.deltaTime;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += Speed * Time.deltaTime;
        }
    }

    void SnapToBubble(Guid id){
        this.transform.position = startPos;
    }
}

