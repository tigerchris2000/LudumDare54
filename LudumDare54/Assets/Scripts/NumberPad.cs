using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberPad : MonoBehaviour
{
    [SerializeField] private string password = "1234";
    [SerializeField] private GameObject numbers;
    [SerializeField] private GameObject greenLight;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private GameObject[] interactives;
    private bool pressedF = false;
    private bool changed = false;
    private bool reset = false;
    private float timer = 0f;
    private string input = "";
    
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
            Time.timeScale = 0f;
            input = "";
            numbers.SetActive(true);
        } 
    }
    
    public void AddNumber(string s) {
        if (reset) {
            input = "";
            textField.color = Color.black;
            reset = false;
        }
        input += s;
        textField.text = input;
        if(input.Length == password.Length) {
            if(input == password) {
                textField.color = Color.green;
                GetComponent<AudioSource>().Play();
                greenLight.SetActive(true);
                ChangeAll();
            } else {
                //Wrong Input
                textField.color = Color.red;
                reset = true;
            }
        }
    }
    public void Close() {
        numbers.SetActive(false);
        Time.timeScale = 1f;
    }

    void ChangeAll() {
        for(int i = 0; i< interactives.Length; i++) {
            interactives[i].GetComponent<IInteractive>().Change();
            changed = true;
        }
    }


}
