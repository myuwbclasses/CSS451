using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlaneEquation : MonoBehaviour
{
    // Assumes initial Vn points in (0, 1, 0): only works with Unity Plane, not Unity Quad
    public Transform PtFromOrigin = null;   // point of the plane along normal from the origin
    public Transform PtAlongNormal = null;  // point indicating the normal vector
    public float NormalPtDistance = 3f;
    public bool ApplyOnQuad = false;

    private Vector3 Vn;  // Normalized
    private float D;     // Distance from origin, along the normal vector direction

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(PtFromOrigin != null);
        Debug.Assert(PtAlongNormal != null);
    }

    // Update is called once per frame
    void Update()
    {
        // After rotation, this is the normal vector
        if (ApplyOnQuad)
            Vn = -transform.forward; // for Unity Quad
        else
            Vn = transform.up;  // for Unity Plane

        // Note that: transform.localPosition is guaranteed to be on the plane!
        D = Vector3.Dot(transform.localPosition, Vn);  // This is distance to 

        // Now let's verify our values
        PtFromOrigin.localPosition = D * Vn;
        PtAlongNormal.localPosition = PtFromOrigin.localPosition + NormalPtDistance * Vn;

    }
}
