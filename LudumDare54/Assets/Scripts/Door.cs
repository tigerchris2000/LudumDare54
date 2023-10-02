using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractive
{
    private bool open = false;
    private Sprite sprite;
    private void Start()
    {
            sprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void Change()
    {
        if(!open) {
            open = !open;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else {
            open = !open;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        GetComponent<BoxCollider2D>().isTrigger = !GetComponent<BoxCollider2D>().isTrigger;
    }
}
