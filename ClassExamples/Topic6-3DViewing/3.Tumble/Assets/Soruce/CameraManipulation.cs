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
    public bool OrbitHorizontal = false;



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
                Vector3 W = Vector3.Cross(-V, transform.up);
                Vector3 U = Vector3.Cross(W, -V);
                transform.localRotation = Quaternion.LookRotation(V, U);
                break;

            case LookAtCompute.TransformLookAt:
                transform.LookAt(LookAtPosition);
                break;
        }

        ComputeHorizontalOrbit(); 
	}

    const float RotateDelta = 10f / 60;  // about 10-degress per second
    float Direction = 1f;
    void ComputeHorizontalOrbit()
    {
        if (!OrbitHorizontal)
            return;

        // orbit with respect to the transform.right axis

        // 1. Rotation of the viewing direction by right axis
        Quaternion q = Quaternion.AngleAxis(Direction * RotateDelta, transform.right);

        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        transform.localPosition = newCameraPos;
        
        // transform.LookAt(LookAtPosition);
        transform.localRotation = q * transform.localRotation;

        if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) > 0.7071f) // this is about 45-degrees
        {
            Direction *= -1f;
        }
    }

}
