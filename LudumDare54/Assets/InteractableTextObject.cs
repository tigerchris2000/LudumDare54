using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableTextObject : MonoBehaviour
{
    bool playerIsInRange = false; 
    bool noteActive = false;
    [SerializeField] int textID = -1;
    [SerializeField] Sprite[] notes;
    [SerializeField] Image noteImage;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Player has entered the range.");
            playerIsInRange = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Player has entered the range.");
            playerIsInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerIsInRange = false;
        }
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && playerIsInRange) {
            ToggleNoteDisplay();
        }
    }
    void ToggleNoteDisplay() {
        if(!noteActive) {
            DisplayNote();
            Time.timeScale = 0;
        } else {
            HideNote();
            Time.timeScale = 1;
        }
        noteActive = !noteActive;
    }
    void DisplayNote() {
        switch(textID) {             
            default:
            case -1: HideNote(); break;
            case 1: noteImage.sprite = notes[textID-1]; break;
        }
        noteImage.gameObject.SetActive(true);
    }
    void HideNote() {
        noteImage.gameObject.SetActive(false);
    }
}
