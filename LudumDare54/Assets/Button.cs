using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject[] interactives; 
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            for(int i = 0; i< interactives.Length; i++) {
                interactives[i].GetComponent<IInteractive>().Change();
            }
        } 
    }
}
