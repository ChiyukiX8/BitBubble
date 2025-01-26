using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayed : MonoBehaviour
{
    public float Delay = 5;
    public void Start()
    {
        Destroy(gameObject, Delay);   
    }
}
