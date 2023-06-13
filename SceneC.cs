using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneC : MonoBehaviour
{
    public void ChangeFirstScene()
    {
        SceneManager.LoadScene("Lound1");
    }
}
