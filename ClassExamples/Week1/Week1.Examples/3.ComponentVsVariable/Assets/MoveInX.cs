using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInX : MonoBehaviour {

    float mDelta = 0.1f;
    float mDir = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition += mDir * new Vector3(mDelta, 0, 0);
        if (Mathf.Abs(transform.localPosition.x) > 10)
            mDir *= -1;
	}

    public void IncDelta() { mDelta = 0.3f;  }
    public void DecDelta() { mDelta = 0.1f; }
}
