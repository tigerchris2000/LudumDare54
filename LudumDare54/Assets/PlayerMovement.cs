using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    enum Dir {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    [SerializeField] private float speed;
    [SerializeField] private float acc;
    [SerializeField] private float dec;
    [SerializeField] private float rachDistance;
    [SerializeField] private float pullingSpace;
    [SerializeField] private LayerMask moveableObjects;
    [SerializeField] Image redFlashImage;
    private float maxSpeed;
    private GameObject player;
    private GameObject pulledObject;
    private Rigidbody2D rb;
    private Vector2 move;
    private bool sneak = false;
    private bool pull = false;
    private Dir looking;
    private Vector3 pulledObjectDir;
    

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        maxSpeed = speed;
    }
    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        sneak = Input.GetKey(KeyCode.LeftShift);

        print(rb.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.G)) {
            Die();
        }
        GetInput();
        Pulling();
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
        SceneManager.LoadScene("DeathImage");
    }
    void GetInput() {
        move.x = Input.GetAxisRaw("Horizontal");
        if (move.x > 0) looking = Dir.RIGHT;
        if (move.x < 0) looking = Dir.LEFT;
        move.y = Input.GetAxisRaw("Vertical");
        if (move.y > 0) looking = Dir.UP;
        if (move.y < 0) looking = Dir.DOWN;
        sneak = Input.GetKey(KeyCode.LeftShift);
    }
    void Pulling() {
        if (!pull) {
            if (Input.GetKey(KeyCode.F) ) {
                Vector2 vec;
                switch (looking) {
                    case Dir.UP:
                        vec = Vector2.up;
                        break;
                    case Dir.DOWN:
                        vec = Vector2.down;
                        break;
                    case Dir.RIGHT:
                        vec = Vector2.right;
                        break;
                    case Dir.LEFT:
                        vec = Vector2.left;
                        break;
                    default:
                        vec = Vector2.zero;
                        break;
                }
                RaycastHit2D ray1 = Physics2D.Raycast(transform.position, vec, rachDistance, moveableObjects);
                RaycastHit2D ray2 = Physics2D.Raycast(transform.position + Vector3.Scale(vec, Vector2.one * 0.1f), vec, rachDistance, moveableObjects);
                RaycastHit2D ray3 = Physics2D.Raycast(transform.position + Vector3.Scale(vec, Vector2.one * 0.2f), vec, rachDistance, moveableObjects);
                RaycastHit2D ray4 = Physics2D.Raycast(transform.position - Vector3.Scale(vec, Vector2.one * 0.1f), vec, rachDistance, moveableObjects);
                RaycastHit2D ray5 = Physics2D.Raycast(transform.position - Vector3.Scale(vec, Vector2.one * 0.2f), vec, rachDistance, moveableObjects);
                if(ray1.collider != null) {
                    pull = true;
                    speed = maxSpeed / 2;
                    pulledObject = ray1.collider.gameObject;
                    pulledObjectDir = vec;
                }
                if(ray2.collider != null) {
                    pull = true;
                    speed = maxSpeed / 2;
                    pulledObject = ray2.collider.gameObject;
                    pulledObjectDir = vec;
                }
                if(ray3.collider != null) {
                    pull = true;
                    speed = maxSpeed / 2;
                    pulledObject = ray3.collider.gameObject;
                    pulledObjectDir = vec;
                }
                if(ray4.collider != null) {
                    pull = true;
                    speed = maxSpeed / 2;
                    pulledObject = ray4.collider.gameObject;
                    pulledObjectDir = vec;
                }
                if(ray5.collider != null) {
                    pull = true;
                    speed = maxSpeed / 2;
                    pulledObject = ray5.collider.gameObject;
                    pulledObjectDir = vec;
                }
            }
        } else {
            if (!Input.GetKey(KeyCode.F) ) {
                speed = maxSpeed;
                pull = false;
                pulledObject = null;
            } else {
                pulledObject.transform.position = transform.position + Vector3.Scale(pulledObjectDir , pulledObject.transform.localScale * 0.5f + Vector3.one * pullingSpace) ; 
            }
        }
    }
}
