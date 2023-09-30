using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneScript : MonoBehaviour
{
    public void loadScene(String sceneName) {
        Debug.Log("Attempting to load Scene " + '\u0022'+sceneName+'\u0022' + "..."); 
        SceneManager.LoadScene(sceneName);
    }
    public void Adios() {
        Application.Quit();
    }
}
