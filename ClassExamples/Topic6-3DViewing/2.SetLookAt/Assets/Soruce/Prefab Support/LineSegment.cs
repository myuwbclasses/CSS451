using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Assume hanging on a cylinder
public class LineSegment : MonoBehaviour {
    private Vector3 mP1, mP2;
    private Vector3 mV;  // direction of the line, normalized
    private float mL;   // Len of the line segment

	// Use this for initialization
	void Start () {
        // transform.localScale = new Vector3(0.1f, 1, 0.1f); // default width
        ComputeLineDetails();
    }
	
	// Update is called once per frame
	void Update () {
        
        // 1. compute the rotation 
       // if (Mathf.Abs(Vector3.Dot(mV, transform.up)) < 0.99999) {  // not already aligned
            Quaternion q = Quaternion.FromToRotation(Vector3.up, mV);
            transform.localRotation = q; 

            // 2. place in the proper place, remembering to shift by Y
            transform.localPosition = mP1 + mV * (mL / 2f);
        //}

	}

    // setters
    public void SetPoints(Vector3 p1, Vector3 p2) {
        mP1 = p1;
        mP2 = p2;
        ComputeLineDetails();
    }

    public void SetBegin(Vector3 p1)
    {
        mP1 = p1;
        ComputeLineDetails();
    }

    public void SetEnd(Vector3 p2)
    {
        mP2 = p2;
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

    // only postive numbers are valid
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

    // Private functions
    private void ComputeLineDetails()
    {
        mV = mP2 - mP1;
        mL = mV.magnitude;
        mV = mV / mL;
        // remember to scale the length of the cylinder
        Vector3 s = transform.localScale;
        s.y = mL / 2f;
        transform.localScale = s;
    }
}