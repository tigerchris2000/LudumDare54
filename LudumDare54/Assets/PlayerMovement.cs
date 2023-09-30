using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float acc;
    [SerializeField] private float dec;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 move;
    private bool sneak = false;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        sneak = Input.GetKey(KeyCode.LeftShift);

        print(rb.velocity.magnitude);
    }
    void FixedUpdate()
    {
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
}
