using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEndPt : MonoBehaviour {

    // One end point of a line segment
    public string RestingWall;
    	
	public bool SetPosition(string onWall, Vector3 pos)
    {
        bool hit = (onWall == RestingWall);
        if (hit)
            transform.localPosition = pos;
        return hit;
    }

    public void SetPosition(Vector3 p)
    {
        transform.localPosition = p;
    }

    public Vector3 GetPosition() { return transform.localPosition; }
}
