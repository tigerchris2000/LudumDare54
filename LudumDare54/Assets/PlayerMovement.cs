using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acc;
    [SerializeField] private float dec;
    [SerializeField] Image redFlashImage;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 move;
    private bool sneak = false;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        sneak = Input.GetKey(KeyCode.LeftShift);

        print(rb.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.G)) {
            Die();
        }
    }
    void FixedUpdate() {
        if(move.magnitude > 0) {
            rb.drag = 0;
            rb.AddForce(move.normalized * acc);
            if (sneak) {
                if (rb.velocity.magnitude > speed * 0.5f) {
                    rb.velocity = rb.velocity.normalized * speed * 0.5f; 
                } 
            } else {
                if (rb.velocity.magnitude > speed) {
                    rb.velocity = rb.velocity.normalized * speed; 
                }
            }
        } else {
            rb.drag = dec;
        }
    }
    public void Die() {
	    StartCoroutine(redFlash());
	    // Do more dying...
    }

    IEnumerator redFlash() {
        redFlashImage.gameObject.SetActive(true);
        int stepCount = 20;
	    for (int i = 0; i < stepCount; i++) {
            Color c = redFlashImage.color;
            Debug.Log("Adding "+(1f/stepCount));
	        c.a += (float)(1f/stepCount);
            redFlashImage.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < stepCount; i++) {
            Color c = redFlashImage.color;
	        c.a -= (float)(1f/stepCount);
            redFlashImage.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        redFlashImage.gameObject.SetActive(false);
    }
}
