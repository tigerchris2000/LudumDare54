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

    [Header("Sound Detection")]
    [SerializeField] private float rotatingSpeed = 1f;
    [SerializeField] private float ignoreSoundTime = 0.5f;
    private bool rotated = false;
    private bool detected = false;
    private Vector3 soundSource;
    private float timeSinceDetected = 0;
    private float timer = 0;

            
    void Start() {
        movePoints.Add(transform.position);
        for(int i = 0; i < transform.childCount; i++) {
            movePoints.Add(transform.GetChild(i).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DoSomething();
    }
    void DoSomething() {
        if (detected)
        {
            rotateTowardsSound();
            if (timeSinceDetected < Time.time)
                detected = false;
        }
        else if (rotated) {
            rotateBack(); 
        }
        else
        {
            moveToNext();
        }
    }
    void moveToNext() {
        print("moving");
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
    void rotateTowardsSound() {
        Vector3 vec = Vector3.zero;
        Vector3 rot = transform.rotation.eulerAngles;
        Vector3 dir = soundSource - transform.position;
        vec.z = Vector3.Angle(dir, Vector3.up); ;
        if (dir.x > 0) vec.z = 360 - vec.z;
        if (vec.z - rot.z > 0 && vec.z - rot.z < 180)
            vec.z = rot.z + Time.deltaTime * rotatingSpeed;
        else if (vec.z - rot.z < 0 && vec.z - rot.z < -180)
            vec.z = rot.z + Time.deltaTime * rotatingSpeed;
        else
            vec.z = rot.z - Time.deltaTime * rotatingSpeed;

        transform.rotation = Quaternion.Euler(vec);
        rotated = true;
    }
    void rotateBack() {
        Vector3 vec = Vector3.zero;
        Vector3 rot = transform.rotation.eulerAngles;
        Vector3 dir = movePoints[next] - transform.position;
        vec.z = Vector3.Angle(dir, Vector3.up);
        
        if(vec.z - rot.z > rotatingSpeed * Time.deltaTime) {
            vec.z = rot.z + rotatingSpeed * Time.deltaTime; 
        }else if(vec.z - rot.z < - rotatingSpeed * Time.deltaTime){
            vec.z = rot.z + rotatingSpeed * Time.deltaTime;
        } else{
            rotated = false;
        }
        rot = vec;
    }
    public void detectSound(GameObject gameObject) {
        detected = true;
        soundSource = gameObject.transform.position;
        timeSinceDetected = Time.time + ignoreSoundTime;
        timer = 0;
    }
}
