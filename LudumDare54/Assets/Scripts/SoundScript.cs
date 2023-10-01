using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private LayerMask hittables;
    [SerializeField] private int rayCount = 36;
    public void send(float range) {
        for(int i = 0; i < 36; i++) {
            float deg = (360 / rayCount) * i;
            deg = deg * Mathf.Deg2Rad;
            Vector2 dir = Vector3.zero;
            dir.x = Mathf.Sin(deg);
            dir.y = Mathf.Cos(deg);
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, range, hittables);
            if(ray.collider != null) {
                ray.collider.gameObject.GetComponent<Enemy>().detectSound(gameObject);
            }
        }
    }
}
