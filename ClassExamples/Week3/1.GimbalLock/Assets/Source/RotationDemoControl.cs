using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDemoControl : MonoBehaviour
{
    public Transform TheCylinder = null;
    public SliderWithEcho X=null, Y=null, Z=null;
    // Start is called before the first frame update
    
    void Start()
    {
        Debug.Assert(TheCylinder != null);
        Debug.Assert(X != null);
        Debug.Assert(Y != null);
        Debug.Assert(Z != null);
        X.SetSliderListener(UpdateXRotate);
        Y.SetSliderListener(UpdateYRotate);
        Z.SetSliderListener(UpdateZRotate);

        // Assuming current X/Y/Z is all zero
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//
    void UpdateXRotate(float v) {
        Vector3 eulerAngles = TheCylinder.localRotation.eulerAngles;
        eulerAngles.x = v;
        TheCylinder.localRotation = Quaternion.Euler(eulerAngles);
    }

    void UpdateYRotate(float v) {
        Vector3 eulerAngles = TheCylinder.localRotation.eulerAngles;
        eulerAngles.y = v;
        TheCylinder.localRotation = Quaternion.Euler(eulerAngles);
    }

    void UpdateZRotate(float v) {
        Vector3 eulerAngles = TheCylinder.localRotation.eulerAngles;
        eulerAngles.z = v;
        TheCylinder.localRotation = Quaternion.Euler(eulerAngles);
    }
}
