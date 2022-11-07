using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderAsLine : MonoBehaviour
{
    public Transform P1 = null, P2 = null;  // the two end points
    public float LineWidth = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P1 != null);
        Debug.Assert(P2 != null);
        P1.localPosition = Vector3.up;
        P2.localPosition = -Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 V = P2.localPosition - P1.localPosition;
        float len = V.magnitude;

        // origin is the center of the line
        // we need to move from origin to current line mid-point
        transform.localPosition = P1.localPosition + 0.5f * V;  // mid point of the line is where we move it
        transform.localScale = new Vector3(LineWidth, len * 0.5f, LineWidth);  // line width and length
        // This is magic! for now ...
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);

    }
}
