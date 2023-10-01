using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject[] interactives;
    private bool pressedF = false;
    private bool changed = false;
    private float timer = 0f;
    
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.F)) {
            pressedF = true;
            changed = false;
            timer = Time.time + 0.1f;
        } else if(Input.GetKeyUp(KeyCode.F)) {
            pressedF = false;
        }
        if (pressedF) {
            timer += Time.deltaTime;
        }
        if(pressedF && timer < Time.time) {
            pressedF = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player") && pressedF && !changed) {
            for(int i = 0; i< interactives.Length; i++) {
                interactives[i].GetComponent<IInteractive>().Change();
                changed = true;
            }
        } 
    }
}
