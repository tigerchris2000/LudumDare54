using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractive
{
    public void Change()
    {
        if(GetComponent<SpriteRenderer>().color == Color.red)
            GetComponent<SpriteRenderer>().color = Color.green;
        else 
            GetComponent<SpriteRenderer>().color = Color.red;
    }
}
