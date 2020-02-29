using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject ThePlane;
    public GameObject P1, P2;
    public GameObject PlaneNormal;
    public GameObject Pt;  // collision point
    

    private void Start()
    {
        P1.GetComponent<Renderer>().material.color = Color.black;
        P2.GetComponent<Renderer>().material.color = Color.black;
        Pt.GetComponent<Renderer>().material.color = Color.red;
    }

    float kNormalSize = 10f;
    float kVerySmall = 0.0001f; // let's avoid this
    private void Update()
    {
        // Draw the line
        Debug.DrawLine(P1.transform.localPosition, P2.transform.localPosition, Color.black);

        // the plane and its normal
        Vector3 n = -ThePlane.transform.forward;
        Vector3 center = ThePlane.transform.localPosition;
        float d = Vector3.Dot(n, center);

        // The line
        Vector3 V = P2.transform.localPosition - P1.transform.localPosition;
        V.Normalize(); // 

        // the interseciton distant
        float denom = Vector3.Dot(n, V);
        if (Mathf.Abs(denom) < kVerySmall)
        {
            // almost parallel, ignore 
            Pt.GetComponent<Renderer>().enabled = false;
            return;
        } else
        {
            Pt.GetComponent<Renderer>().enabled = true;
        }

        // intersection distant
        float t1 = (d - Vector3.Dot(n, P1.transform.localPosition)) / denom;
        Pt.transform.localPosition = P1.transform.localPosition + t1 * V;
        
        // normal
        PlaneNormal.transform.localRotation = Quaternion.FromToRotation(Vector3.up, n);
        PlaneNormal.transform.localPosition = ThePlane.transform.localPosition + (kNormalSize * PlaneNormal.transform.up);

        // What if we only want to draw the red sphere when the intereseciton is actually inside the line segment?
    }
}
