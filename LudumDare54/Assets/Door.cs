using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractive
{
    private bool open = false;
    public void Change()
    {
        if(!open) {
            open = !open;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else {
            open = !open;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        GetComponent<BoxCollider2D>().isTrigger = !GetComponent<BoxCollider2D>().isTrigger;
    }
}
