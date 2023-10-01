using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetActive : MonoBehaviour
{
    [SerializeField] List<GameObject> toSetActive;
    float freezeDuration = 1f;
    float releaseTime;
    bool movFrozen = false;
    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update() {
        if (Time.time > releaseTime && movFrozen) {
            // player.GetComponent<PlayerMovement>().SetMovementEnabled(true);
            movFrozen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        // Short Freeze
        releaseTime = Time.time + freezeDuration;
        // player.GetComponent<PlayerMovement>().SetMovementEnabled(false);
        movFrozen = true;

        foreach (GameObject g in toSetActive) {
            g.SetActive(true);
        }
    }

}
