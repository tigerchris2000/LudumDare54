using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float offset = 1;
    private List<Vector3> movePoints = new List<Vector3>();
    private bool reverse = false;
    private int next = 0;

            
    void Start() {
        movePoints.Add(transform.position);
        for(int i = 0; i < transform.childCount; i++) {
            movePoints.Add(transform.GetChild(i).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveToNext();        
    }
    void moveToNext() {
        if( Vector3.Distance(transform.position,movePoints[next]) < offset) {
            if (reverse) {
                if(next == 0) {
                    reverse = false;
                    next = 1;
                } else {
                    next--;
                }
            } else {
                if(next == movePoints.Count - 1) {
                    reverse = true;
                    next--;
                }
                else {
                    next++;
                }
            }
        }

        Vector3 tmp = movePoints[next] - transform.position;
        tmp.Normalize();
        tmp = tmp * speed * Time.deltaTime;
        transform.position = transform.position + tmp;
        rotate(tmp);
    }
    void rotate(Vector3 target) {
        Vector3 vec = Vector3.zero;
        vec.z = Vector3.Angle(target, Vector3.up); ;
        if (target.x < 0) vec.z *= -1; vec.z *= -1;
        transform.rotation = Quaternion.Euler(vec);
    }
}
