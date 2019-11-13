#define OurOwnRotation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Assume hanging on a cylinder
public class UILineSegment : LineSegment {
    public LineEndPt mEndP1;
    public LineEndPt mEndP2;

	// Use this for initialization
	void Start () {
        Debug.Assert(mEndP1 != null);
        Debug.Assert(mEndP2 != null);
        base.SetEndPoints(mEndP1.GetPosition(), mEndP2.GetPosition());
        ComputeLineDetails();
    }

    // setters
    public override void SetEndPoints(Vector3 p1, Vector3 P2)
    {
        base.SetEndPoints(p1, P2);
        mEndP1.SetPosition(p1);
        mEndP2.SetPosition(P2);
    }

    public bool SetEndPoint(string onWall, Vector3 p)
    {
        bool hit = (mEndP1.SetPosition(onWall, p) || mEndP2.SetPosition(onWall, p));
        if (hit)
        {
            base.SetEndPoints(mEndP1.transform.localPosition, mEndP2.transform.localPosition);
            ComputeLineDetails();
        }
        return hit;
    }
    
}