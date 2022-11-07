using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    // Vector3 mN;     // normal, normalized, this is actually -transform.forward
    float mD;       // mN dot P = mD;
    public LineSegment mNormal; // This is the normal vector we will display

    const float kNormalLength = 5f; // length of the normal vector

	// Update is called once per frame
	void Update () {
        UpdatePlaneEquation();
        Vector3 center = transform.localPosition;
        Vector3 p1 = center + kNormalLength * GetNormal();
        mNormal.SetEndPoints(center, p1);
    }

    public void UpdatePlaneEquation()
    {
        mD = Vector3.Dot(transform.localPosition, GetNormal());
    }

    public Vector3 GetNormal() { return -transform.forward; }
    public float GetD() { return mD;  }

    public float DistantToPoint(Vector3 p)
    {
        return  Vector3.Dot(p, GetNormal()) - mD;
    }

    public Vector3 ReflectFromPlane(Vector3 v)
    {
        v = -v;
        float vDotn = Vector3.Dot(v, GetNormal());
        return 2 * vDotn * GetNormal() - v;
    }

    public bool InActiveZone(Vector3 p)
    {
        Vector3 d = p - transform.localPosition;
        return d.magnitude < (transform.localScale.x / 2f);
    }

    public bool PtInfrontOf(Vector3 p)
    {
        Vector3 va = p - transform.localPosition;
        return (Vector3.Dot(va, GetNormal()) > 0f);
    }
}