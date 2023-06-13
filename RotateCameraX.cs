﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 300;
    public GameObject player;
    //private Vector3 offset=new Vector3(0,2,0);


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        //transform.position = player.transform.position + offset;

        //transform.position = player.transform.position; // Move focal point with player

    }
}