using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneScript : MonoBehaviour
{
    [SerializeField] string sceneName = "";

    public void loadSceneDirect(String sceneNameDirect) {
        Debug.Log("Attempting to load Scene " + '\u0022'+sceneNameDirect+'\u0022' + "..."); 
        SceneManager.LoadScene(sceneNameDirect);
    } 
    public void loadScene() {
        Debug.Log("Attempting to load Scene " + '\u0022'+sceneName+'\u0022' + "..."); 
        SceneManager.LoadScene(sceneName);
    }
    public void Adios() {
        Application.Quit();
    }
    void OnTriggerEnter2D(Collider2D c) {
        if(c.CompareTag("Player")) {
            loadScene();
        }
    }
}
