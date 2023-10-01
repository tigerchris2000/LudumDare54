using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask objects;
    [SerializeField]
    private int rayCount = 10;
    private GameObject player;
    [SerializeField]
    private GameObject[] lights;

    private List<RaycastHit2D> rays = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lights = GameObject.FindGameObjectsWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        CalcRays();
        CheckRays();
    }
    void CheckRays() { 
       for(int i = 0; i < rays.Count; i++) {
            Collider2D collider = rays[i].collider;
                                

            if(collider != null) {
                if (collider.CompareTag("Player")) {
                     //Debug.Log("DIE");
                    //player.GetComponent<PlayerMovement>().Die();
                }
            }
       }
    }
    void CalcRays() {
        rays = new List<RaycastHit2D>();
        for (int i = 0; i < lights.Length; i++) {
            Light2D light = lights[i].GetComponent<Light2D>();
            switch (light.lightType) {
                case Light2D.LightType.Global:
                    break;
                case Light2D.LightType.Sprite:
                    break;
                case Light2D.LightType.Freeform:
                    break;
                case Light2D.LightType.Point:
                    float innerAngle = Mathf.Deg2Rad * light.pointLightInnerAngle * 0.5f;
                    float outerAngle = Mathf.Deg2Rad * light.pointLightOuterAngle * 0.5f;

                    float innerRad = light.pointLightInnerRadius;
                    float outerRad = light.pointLightOuterRadius;

                    float deg = 360 - lights[i].transform.rotation.eulerAngles.z;
                    deg = deg * Mathf.Deg2Rad;
                    float x = Mathf.Sin(deg);
                    float y = Mathf.Cos(deg); 

                    Vector2 straight = new Vector2(x, y);
                    x = Mathf.Sin(deg + outerAngle);
                    y = Mathf.Cos(deg + outerAngle);
                    Vector2 right = new Vector2(x, y);
                    x = Mathf.Sin(deg - outerAngle);
                    y = Mathf.Cos(deg - outerAngle);
                    Vector2 left = new Vector2(x, y);
                    RaycastHit2D rayhitLeft = Physics2D.Raycast(lights[i].transform.position, left, outerRad, objects);
                    RaycastHit2D rayhitRight = Physics2D.Raycast(lights[i].transform.position, right, outerRad, objects);
                    RaycastHit2D rayhitStraight = Physics2D.Raycast(lights[i].transform.position, straight, outerRad, objects);
                    rays.Add(rayhitLeft);
                    rays.Add(rayhitRight);
                    rays.Add(rayhitStraight);

                    Vector2 pos = lights[i].transform.position;
                    float step = (outerAngle * 2) / (rayCount - 3);
                    for (int j = 0; j < rayCount - 3; j++) { 
                        x = Mathf.Sin(deg - outerAngle + step * j);
                        y = Mathf.Cos(deg - outerAngle + step * j);
                        Vector2 vec = new Vector2(x, y);
                        RaycastHit2D rayhitVec = Physics2D.Raycast(lights[i].transform.position, vec, outerRad, objects);
                        rays.Add(rayhitVec);
                    }
                    break; 
            }
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < lights.Length; i++) {
            Light2D light = lights[i].GetComponent<Light2D>();
            switch (light.lightType) {
                case Light2D.LightType.Global:
                    break;
                case Light2D.LightType.Sprite:
                    break;
                case Light2D.LightType.Freeform:
                    break;
                case Light2D.LightType.Point:
                    float innerAngle = Mathf.Deg2Rad * light.pointLightInnerAngle * 0.5f;
                    float outerAngle = Mathf.Deg2Rad * light.pointLightOuterAngle * 0.5f;

                    float innerRad = light.pointLightInnerRadius;
                    float outerRad = light.pointLightOuterRadius;

                    float deg = 360 - lights[i].transform.rotation.eulerAngles.z;
                    deg = deg * Mathf.Deg2Rad;
                    float x = Mathf.Sin(deg);
                    float y = Mathf.Cos(deg); 

                    Vector2 straight = new Vector2(x, y);
                    x = Mathf.Sin(deg + outerAngle);
                    y = Mathf.Cos(deg + outerAngle);
                    Vector2 right = new Vector2(x, y);
                    x = Mathf.Sin(deg - outerAngle);
                    y = Mathf.Cos(deg - outerAngle);
                    Vector2 left = new Vector2(x, y);
                    Vector2 pos = lights[i].transform.position;
                    Gizmos.DrawLine(pos, straight * outerRad + pos);
                    Gizmos.DrawLine(pos, left * outerRad + pos);
                    Gizmos.DrawLine(pos, right * outerRad + pos);
                    float step = (outerAngle * 2) / (rayCount - 3);
                    for (int j = 0; j < rayCount - 3; j++) { 
                        x = Mathf.Sin(deg - outerAngle + step * j);
                        y = Mathf.Cos(deg - outerAngle + step * j);
                        Vector2 vec = new Vector2(x, y);
                        Gizmos.DrawLine(pos, vec * outerRad + pos);
                    }

                    //Ray2D rayLeft = new Ray2D(lights[i].transform.position);                    
                    //Ray2D rayRight = new Ray2D(lights[i].transform.position);                    break; } } } } 
                    break; 
            }
        }
        
    }
}
