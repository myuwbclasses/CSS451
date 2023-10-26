using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePrimitive : MonoBehaviour
{
    // Rotation support
    private const float MaxDegree = 90f;
    private float DeltaDegree = 45f; // per sec
    private float degree = 0f;
    // Start is called before the first frame update

    // Translation support
    private const float MaxDist = 3f;
    private float DeltaMove = 1f;  // 1 unit per section
    private float dist = 0;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        degree += DeltaDegree * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(degree, Vector3.right);
        if ((degree > MaxDegree) || (degree < (-MaxDegree))) {
            DeltaDegree *= -1f;
            degree += DeltaDegree * Time.deltaTime; // ensure we don't get stuck
        }

        // translation
        dist += DeltaMove * Time.deltaTime;
        Vector3 p = transform.localPosition;
        p.x = dist;
        transform.localPosition = p;
        if ((dist > MaxDist) || (dist <(-MaxDist))) {
            DeltaMove *= -1f;
            dist += DeltaMove * Time.deltaTime;
        }
    }
}
