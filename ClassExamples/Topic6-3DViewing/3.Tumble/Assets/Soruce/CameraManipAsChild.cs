using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assumptions:
//     1. Camera is Child-0 of this node
//     2. Camera transform is under complete control of this node
public class CameraManipAsChild : MonoBehaviour
{
    public Transform LookAtPosition = null;
    public LineSegment LineOfSight = null;

    public bool OrbitHorizontal = false;
    // Start is called before the first frame update

    // keep track of state of the camera
    private float mViewDistant = 25f; // distance from us (the parent)
    private float mYDegree = 25f;    // Rotation WRT to x-axis (the y-angle)

    private const float RotateDelta = 10f;  // about 10-degress per second
    private float Direction = 1f;
    void Start()
    {
        Debug.Assert(LookAtPosition != null);
        Debug.Assert(LineOfSight != null);
        LineOfSight.SetWidth(0.2f);
        LineOfSight.SetPoints(transform.localPosition, LookAtPosition.localPosition);
        UpdateCameraTransform();
    }

    // Update is called once per frame
    void Update()
    {
        LineOfSight.SetPoints(GetCameraXform().position, LookAtPosition.localPosition);
        UpdateCameraTransform();

        if (OrbitHorizontal) {
            mYDegree += Direction * RotateDelta * Time.deltaTime;
            if (Mathf.Abs(mYDegree) > 45f)
                Direction *= -1;
        }
    }

    private void UpdateCameraTransform() {
        // 1. look at position
        transform.localPosition = LookAtPosition.localPosition;

        // 2. Place the camera
        Transform ct = GetCameraXform(); // ct is the camera transform
        ct.localPosition = new Vector3(0, 0, -mViewDistant);  // place the camera along the z-axis

        // Now rotate myself to orbit the child
        transform.localRotation = Quaternion.AngleAxis(mYDegree, Vector3.right);  // rotate about the X-axis
    }

    Transform GetCameraXform() { return transform.GetChild(0); }
}
