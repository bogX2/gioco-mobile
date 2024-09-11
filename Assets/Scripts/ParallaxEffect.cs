using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;


    Vector2 startingPosition;
    float startingZ;

Vector2 camMoveSinceStart=>(Vector2)cam.transform.position-startingPosition;

float ZdistanceFromTarget=>transform.position.z -followTarget.transform.position.z;

float clippingPlane => ( cam.transform.position.z+ (ZdistanceFromTarget >0 ?  cam.farClipPlane:cam.nearClipPlane));

float ParallaxFactor=>Mathf.Abs(ZdistanceFromTarget)/clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition=transform.position;
        startingZ=transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition=startingPosition+camMoveSinceStart*ParallaxFactor;
        transform.position= new Vector3(newPosition.x,newPosition.y,startingZ);
    }
}
