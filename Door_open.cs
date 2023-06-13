using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// https://assetstore.unity.com/packages/3d/props/interior/tim-s-horror-assets-the-bloody-door-70847#reviews
public class Door_open : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //  ¿­±â
        this.GetComponent<Animator>().SetBool("open", true); //open 

        //´Ý±â
        // this.GetComponent<Animator>().SetBool("open", false); //close 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
