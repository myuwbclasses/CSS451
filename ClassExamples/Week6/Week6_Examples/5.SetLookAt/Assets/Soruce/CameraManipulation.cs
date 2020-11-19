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
	
	// Update is called once per frame
	void Update () {
        LineOfSight.SetPoints(transform.localPosition, LookAtPosition.localPosition);

        switch (ComputeMode)
        {
            case LookAtCompute.QuatLookRotation:
                // Viewing vector is from transform.localPosition to the lookat position
                Vector3 V = LookAtPosition.localPosition - transform.localPosition;
                Vector3 W = Vector3.Cross(-V, Vector3.up);
                Vector3 U = Vector3.Cross(W, -V);
                // transform.localRotation = Quaternion.LookRotation(V, U);
                transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
                Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
                transform.localRotation = alignU * transform.localRotation;
                break;

            case LookAtCompute.TransformLookAt:
                transform.LookAt(LookAtPosition);
                break;
        }
	}
}
