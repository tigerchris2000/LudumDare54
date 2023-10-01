using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool playerInPickupRange;
    GameObject player;
    [SerializeField] string itemName; 

    void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Player")) {
            player = c.gameObject;
            playerInPickupRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D c) {
        if (c.CompareTag("Player")) {
            playerInPickupRange = false;
        }
    }
    void Update() {
        if(playerInPickupRange && Input.GetKeyDown(KeyCode.F)) {
            PickUp();
        }
    }
    void PickUp() {
        player.GetComponent<PlayerItemHandler>().SetCurrentItem(itemName);
        Destroy(gameObject);
    }
}
