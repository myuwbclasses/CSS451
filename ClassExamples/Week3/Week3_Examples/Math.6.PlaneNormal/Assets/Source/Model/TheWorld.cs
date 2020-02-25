using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject ThePlane;
    public GameObject PtOnPlane;
    

    private void Start()
    {
        PtOnPlane.GetComponent<Renderer>().material.color = Color.black;
    }

    float kSize = 3f;
    private void Update()
    {
        Vector3 n = -ThePlane.transform.forward;  // Interesting?

        // Alternate way of getting the normal ...
        //    No worries, we will learn about this!
        Quaternion q = ThePlane.transform.localRotation;
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, q, Vector3.one); // Rotation matrix
        n = -m.GetColumn(2);

        Vector3 center = ThePlane.transform.localPosition;
        float d = Vector3.Dot(n, center);

        Vector3 pt = center + kSize * n;
        Debug.DrawLine(center, pt, Color.white);  // normal at the center

        PtOnPlane.transform.localPosition = Vector3.zero + d * n; // line from the origin to where the plane is! 
        Debug.DrawLine(Vector3.zero, PtOnPlane.transform.localPosition, Color.black);
        // for debugging: Debug.Log(hit + ":" + details);
        
    }
}
