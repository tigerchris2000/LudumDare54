using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acc;
    [SerializeField] private float dec;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 move;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if(move.magnitude > 0) {
            rb.drag = 0;
            rb.AddForce(move * acc);
            if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        } else {
            rb.drag = dec;
        }
    }
    void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
