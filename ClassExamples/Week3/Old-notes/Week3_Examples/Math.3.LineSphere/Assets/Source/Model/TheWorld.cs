using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject P1, P2;  // defines the line
    public GameObject TheSphere;  // here is the sphere
    public GameObject Pa, Pb;  // The two intersections

    private void Start()
    {
        TheSphere.GetComponent<Renderer>().material.color = Color.red;
        Pa.GetComponent<Renderer>().material.color = Color.black;
        Pb.GetComponent<Renderer>().material.color = Color.black;
    }

    private void Update()
    {
        bool hit = false;
        string details = "no hit";
        Vector3 V = P2.transform.localPosition - P1.transform.localPosition;
        float len = V.magnitude;
        V /= len;
        Vector3 X = TheSphere.transform.localPosition - P1.transform.localPosition;
        float h = Vector3.Dot(X, V);

        float r = TheSphere.transform.localScale.x * 0.5f; // assume all the same

        Vector3 ph;
        float d, a;
        if (h >= len) // case A
        {
            if ((P2.transform.localPosition-TheSphere.transform.localPosition).magnitude < r) {
                hit = true;
                details = "Case A";

                ph = P1.transform.localPosition + h * V;
                d = (TheSphere.transform.localPosition - ph).magnitude;
                a = Mathf.Sqrt(r * r - d * d);
                Pa.transform.localPosition = P1.transform.localPosition + (h - a) * V;
            } else {
                Pa.transform.localPosition = TheSphere.transform.localPosition;
            }
            Pb.transform.localPosition = Pa.transform.localPosition;
        } else if (h <= 0) // case B
        {
            if ((P1.transform.localPosition - TheSphere.transform.localPosition).magnitude < r)
            {
                hit = true;
                details = "Case B";

                ph = P1.transform.localPosition + h * V;
                d = (TheSphere.transform.localPosition - ph).magnitude;
                a = Mathf.Sqrt(r * r - d * d) + h;
                Pa.transform.localPosition = P1.transform.localPosition + a * V;
                
            } else
            {
                Pa.transform.localPosition = TheSphere.transform.localPosition;
            }
            Pb.transform.localPosition = Pa.transform.localPosition;

        } else
        {
            d = Mathf.Sqrt(X.sqrMagnitude - (h * h));
            if (d < r)
            {
                hit = true;
                details = "Case C";

                a = Mathf.Sqrt(r * r - d * d);
                Pa.transform.localPosition = P1.transform.localPosition + (h - a) * V;
                Pb.transform.localPosition = P1.transform.localPosition + (h + a) * V;
            }
            else
            {
                Pa.transform.localPosition = TheSphere.transform.localPosition;
                Pb.transform.localPosition = Pa.transform.localPosition;
            }
        }

        // Draw Debug lines
        Debug.DrawLine(P1.transform.localPosition, P2.transform.localPosition, Color.black);
        // for debugging: Debug.Log(hit + ":" + details);
        
    }
}
