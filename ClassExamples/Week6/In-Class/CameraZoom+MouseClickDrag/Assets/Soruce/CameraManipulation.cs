using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulation : MonoBehaviour {

    public enum LookAtCompute {
        QuatLookRotation = 0,
        TransformLookAt = 1
    };

    public Transform LookAtPosition = null;
    public LineSegment LineOfSight = null;
    public LookAtCompute ComputeMode = LookAtCompute.QuatLookRotation;

	// Use this for initialization
	void Start () {
        Debug.Assert(LookAtPosition != null);
        Debug.Assert(LineOfSight != null);
        LineOfSight.SetWidth(0.2f);
        LineOfSight.SetPoints(transform.localPosition, LookAtPosition.localPosition);
	}
    Vector3 delta = Vector3.zero;
    Vector3 mouseDownPos = Vector3.zero;


	// Update is called once per frame
	void Update () {
        LineOfSight.SetPoints(transform.localPosition, LookAtPosition.localPosition);

        switch (ComputeMode)
        {
            case LookAtCompute.QuatLookRotation:
                // Viewing vector is from transform.localPosition to the lookat position
                Vector3 V = LookAtPosition.localPosition - transform.localPosition;
                Vector3 W = Vector3.Cross(-V, transform.up);
                Vector3 U = Vector3.Cross(W, -V);
                transform.localRotation = Quaternion.LookRotation(V, U);
                break;

            case LookAtCompute.TransformLookAt:
                transform.LookAt(LookAtPosition);
                break;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
            delta = Vector3.zero;
        } if (Input.GetMouseButton(0))
        {
            delta = mouseDownPos - Input.mousePosition;
            mouseDownPos = Input.mousePosition;
            ProcesssZoom(delta.y);
        }
	}

    public void ProcesssZoom(float delta)
    {
        Vector3 v = LookAtPosition.localPosition - transform.localPosition;
        float dist = v.magnitude;
        dist += delta;
        transform.localPosition = LookAtPosition.localPosition - dist * v.normalized;
    }
}
