using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InteractableTextObject : MonoBehaviour
{
    bool playerIsInRange = false; 
    bool textActive = false;
    [SerializeField] int noteNumber = -1;
    [SerializeField] int textID = -1;
    [SerializeField] Image textImage;
    [SerializeField] Sprite[] texts;

    void Start() {
        /* Maybe...
        int[] noteTextIDs = {1, 2, 3, 4, 5};
        for (int i = 0; i < noteTextIDs.Length; i++) {
            if (textID == noteTextIDs[i]) {
                noteNumber = i;
            }
        } */
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
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
            ToggleTextDisplay();
            if (noteNumber != -1 && !STATICStatTracker.noteStatuses.Get(noteNumber)) {
                STATICStatTracker.noteStatuses.Set(noteNumber, true);
            }
        }
    }
    void ToggleTextDisplay() {
        if(!textActive) {
            DisplayTextImage();
            Time.timeScale = 0;
        } else {
            HideTextImage();
            Time.timeScale = 1;
        }
        textActive = !textActive;
    }
    void DisplayTextImage() {
        switch(textID) {             
            default:
            case -1: HideTextImage(); break;
            case 1: textImage.sprite = texts[textID-1]; break;
        }
        textImage.gameObject.SetActive(true);
    }
    void HideTextImage() {
        textImage.gameObject.SetActive(false);
    }
}
