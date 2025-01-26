using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public float Speed = 5.0f;
    public float zoomSpeed = 50f;
    public Vector3 startPos;

    public static CameraController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable(){
        AppEvents.OnBubbleClick.OnTrigger += SnapToBubble;
    }

    private void OnDisable(){
        AppEvents.OnBubbleClick.OnTrigger -= SnapToBubble;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateBubblePanel.IsOpen) return;

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
            Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += zoomSpeed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && CurrencyManager.Instance.CurrentBubbles.Count > 0)
        {
            SnapToBubble(CurrencyManager.Instance.CurrentBubbles.Keys.ToList()[0]);
        }
    }

    public void SnapToBubble(Guid id)
    {
        Bubble bubble = CurrencyManager.Instance.BubbleLookup(id);
        transform.position = new Vector3(bubble.transform.position.x, bubble.transform.position.y, transform.position.z);
    }
}

