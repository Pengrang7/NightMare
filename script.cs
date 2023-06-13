using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    void Awake()

    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.SetResolution(900, 380, true);

    }
}
