using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneScript : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    string[] levelNames = {"StartFR", "Area2"};

    public void loadSceneDirect(String sceneNameDirect) {
        Debug.Log("Attempting to load Scene " + '\u0022'+sceneNameDirect+'\u0022' + "..."); 
        trackScene(sceneNameDirect);
        SceneManager.LoadScene(sceneNameDirect);
    } 
    public void loadScene() {
        Debug.Log("Attempting to load Scene " + '\u0022'+sceneName+'\u0022' + "..."); 
        loadSceneDirect(sceneName);
    }
    public void Adios() {
        Application.Quit();
    }
    void OnTriggerEnter2D(Collider2D c) {
        if(c.CompareTag("Player")) {
            loadScene();
        }
    }
    void trackScene(string newScene) {
        if(levelNames.Contains(newScene)) {
            STATICStatTracker.lastLevel = newScene;
        }
    }
    public void ReloadLastVisitedLevel() {
        if(STATICStatTracker.lastLevel != "") {
            SceneManager.LoadScene(STATICStatTracker.lastLevel);
        } else {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
