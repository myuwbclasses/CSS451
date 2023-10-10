using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDemoControl : MonoBehaviour
{
    public bool UseEulerAsXRef = false;
    public Transform TheCylinder = null;
    public SliderWithEcho X=null, Y=null, Z=null;
    // Start is called before the first frame update

    private float CurrentX = 0, CurrentY = 0, CurrentZ = 0;
    void Start()
    {
        Debug.Assert(TheCylinder != null);
        Debug.Assert(X != null);
        Debug.Assert(Y != null);
        Debug.Assert(Z != null);
        X.SetSliderListener(UpdateXRotate);
        Y.SetSliderListener(UpdateYRotate);
        Z.SetSliderListener(UpdateZRotate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// NOTE: instead of 
//      1. compute q based on the  rotated axes and 
//      2. Concatenate the q on the left side of localRotation
//
// Since the axes are all major axes, we can 
//      1. compute q based on the major axes (before the rotation)
//      2. concatenate the q on the right side of localRotation
//
// we CANNOT read EulerAngles from the transform and update the values
// because, each Quaternion rotation will result in changing of all three Euler's angle
// 
    void UpdateXRotate(float v) {
        if (UseEulerAsXRef) {
            // this does not work as qx does _NOT_ translate to smooth changes
            // of x-value of euler angles
            // Will observe rotation about x-axis, but may have discontinuities
            Vector3 eulerAngles = TheCylinder.localRotation.eulerAngles;
            float delta = v - eulerAngles.x; 
            Quaternion qx = Quaternion.AngleAxis(delta, Vector3.right);  // right is (1, 0, 0)
            TheCylinder.localRotation = TheCylinder.localRotation * qx;
                        
        } else {
            float delta = v - CurrentX; 
            CurrentX = v;
            Quaternion qx = Quaternion.AngleAxis(delta, Vector3.right);  // up is (0, 1, 0)
            TheCylinder.localRotation = TheCylinder.localRotation * qx;
        }
    }

    void UpdateYRotate(float v) {
        float delta = v - CurrentY; 
        CurrentY = v;
        Quaternion qy = Quaternion.AngleAxis(delta, Vector3.up);  // up is (0, 1, 0)
        TheCylinder.localRotation = TheCylinder.localRotation * qy;
    }

    void UpdateZRotate(float v) {
        float delta = v - CurrentZ; 
        CurrentZ = v;
        Quaternion qz = Quaternion.AngleAxis(delta, Vector3.forward);  // forward is (0, 0, 1)
        TheCylinder.localRotation = TheCylinder.localRotation * qz;
    }
}
