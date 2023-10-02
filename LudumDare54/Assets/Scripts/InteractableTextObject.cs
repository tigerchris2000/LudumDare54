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
    [SerializeField] TextMeshProUGUI tmpText;
    [SerializeField] Image textImage;

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
            case 3: tmpText.SetText(STATICStrings.note3Text); break;
        }
        textImage.gameObject.SetActive(true);
    }
    void HideTextImage() {
        textImage.gameObject.SetActive(false);
    }
}
