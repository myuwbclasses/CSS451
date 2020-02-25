using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {
    public Transform PlaneXform;      // Xform of the plane
    public GameObject C0, C1, C2, C3; // these are the four corners
                                      // These four objects should have materials with 451-ShaderNoModelXform
                                      // We will perform ModelXfrom here! (on the CPU side)
    Vector3[] Corners = {
        new Vector3( 0.5f, 0,  0.5f),
        new Vector3( 0.5f, 0, -0.5f),
        new Vector3(-0.5f, 0, -0.5f),
        new Vector3(-0.5f, 0,  0.5f)
    };
    
    private void Start()
    {
        Debug.Assert(PlaneXform != null);
        Debug.Assert(C0 != null);
        Debug.Assert(C1 != null);
        Debug.Assert(C2 != null);
        Debug.Assert(C3 != null);
    }

    private void Update()
    {
        Quaternion q = Quaternion.AngleAxis(-90, Vector3.right);  // undo the inital 90-degree by X-axis of the Quad
        Quaternion rot = PlaneXform.localRotation * q;
        Matrix4x4 m = Matrix4x4.TRS(PlaneXform.localPosition, rot, PlaneXform.localScale);
        

        C0.transform.localPosition = m.MultiplyPoint(Corners[0]);
        C0.transform.localRotation = rot;

        C1.transform.localPosition = m.MultiplyPoint(Corners[1]);
        C1.transform.localRotation = rot;

        C2.transform.localPosition = m.MultiplyPoint(Corners[2]);
        C2.transform.localRotation = rot;

        C3.transform.localPosition = m.MultiplyPoint(Corners[3]);
        C3.transform.localRotation = rot;
    }
}
