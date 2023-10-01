using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    string currentItem = "";
    [SerializeField] List<GameObject> itemList;

    public void SetCurrentItem(string s) {currentItem = s;}
    public string GetCurrentItem() {return currentItem;}
    public void DropCurrentItem() {
        switch (currentItem) {
            // case "key1": Instantiate(itemList[0], transform.position, Quaternion.identity); break;
            case "": 
            default: return;
        }

    }
}
