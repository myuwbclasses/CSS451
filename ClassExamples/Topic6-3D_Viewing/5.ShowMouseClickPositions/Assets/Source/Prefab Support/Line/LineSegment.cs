// #define OurOwnRotation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Assume hanging on a cylinder
public class LineSegment : MonoBehaviour {
    protected Vector3 mP1 = Vector3.zero;
    protected Vector3 mP2 = Vector3.one;

    protected Vector3 mV;  // direction of the line, normalized
    protected float mL;   // Len of the line segment

    public static LineSegment CreateLineSegment()
    {
        return CreateLineSegment(Vector3.zero, Vector3.one);
    }

    public static LineSegment CreateLineSegment(Vector3 p1, Vector3 p2)
    {
        LineSegment l = GameObject.CreatePrimitive(PrimitiveType.Cylinder).AddComponent<LineSegment>();
        l.SetEndPoints(p1, p2);
        return l;
    }

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        ComputeLineDetails();
    }

    public virtual void SetEndPoints(Vector3 p1, Vector3 P2)
    {
        mP1 = p1;
        mP2 = P2;
        ComputeLineDetails();
    }
	
    public void SetWidth(float w)
    {
        Vector3 s = transform.localScale;
        s.x = s.z = w;
        transform.localScale = s;
    }

    // Getters
    public float GetLineLength() { return mL; }
    public Vector3 GetLineDir() { return mV;  }
    public Vector3 GetStartPos() {  return mP1; }
    public Vector3 GetEndPos() {  return mP2; }
    public float GetLineSegmentLength() { return mL; }

    // Return: negative when there is no valid projection
    //         Only projections within the line segment are valid
    public float DistantToPoint(Vector3 p, out Vector3 ptOnLine)
    {
        Vector3 va = p - mP1;
        float h = Vector3.Dot(va, mV);

        float d = 0f;
        ptOnLine = Vector3.zero;

        if ((h < 0) || (h > mL)) { 
            d = -1; // not valid
        } else
        {
            d = Mathf.Sqrt(va.sqrMagnitude - h * h);
            ptOnLine = mP1 + h * mV;
        }
        return d;
    }

    // Compute the line direction/length and move the cylinder to the proper place
    protected void ComputeLineDetails()
    {
        mV = mP2 - mP1;
        mL = mV.magnitude;
        mV = mV / mL;
        // remember to scale the length of the cylinder
        Vector3 s = transform.localScale;
        s.y = mL / 2f;
        transform.localScale = s;

        // 1. compute the rotation 
#if OurOwnRotation
        // form is transform.up;
        // To is current mV
        // make sure the two is not already aligned
        Vector3 up = transform.TransformDirection(Vector3.up);
        // exactly the same as: 
        //      up = transform.up;
        if (Vector3.Dot(up, mV) < 0.9999)  // Angle between the two is not negligible ...
        {
            Vector3 n = Vector3.Cross(up, mV);
            float theta = Mathf.Acos(Vector3.Dot(up, mV)) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(theta, n);
            transform.localRotation = q * transform.localRotation;
        }

#else
        Quaternion q = Quaternion.FromToRotation(Vector3.up, mV);
        transform.localRotation = q;
#endif
        // 2. place in the proper place, remembering to shift by Y
        transform.localPosition = mP1 + mV * (mL / 2f);
    }
}