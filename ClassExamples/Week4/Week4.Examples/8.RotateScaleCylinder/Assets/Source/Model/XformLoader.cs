using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    public Vector3 PivotOffset = Vector3.zero;   // Pivot X/Y/Z
    public bool ShowRotation = false;
    public bool ShowScale = false;

    public Transform DrawPivot;
    private Material mMaterial;  // keep a reference for convenience/efficiency sake!

    // to support slow rotation about the y-axis
    private const float kRotateDelta = 45; // per second
    private Vector3 kScaleDelta = new Vector3(0.1f, 0.1f, 0.1f); // per second
    private const float kMaxSize = 5;
    private const float kMinSize = 0.2f;
    private float IncSign = 1;

    // for comparison purposes
    public Transform DefaultCylinder;

   
	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

        IncrementXform();
        Matrix4x4 m = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        DrawPivot.localRotation = transform.localRotation;
        DrawPivot.localPosition = transform.localPosition + PivotOffset;
        Matrix4x4 pm = Matrix4x4.identity;  // pivot translation
        pm[12] = PivotOffset.x;    // col-3, row-0
        pm[13] = PivotOffset.y;    // col-3, row-1
        pm[14] = PivotOffset.z;    // col-3, row-2

        Matrix4x4 ipm = Matrix4x4.identity;  // inverse pivot translation
        ipm[12] = -pm[12];    // col-3, row-0
        ipm[13] = -pm[13];    // col-3, row-1
        ipm[14] = -pm[14];    // col-3, row-2

        m = pm * m * ipm;    

        mMaterial.SetMatrix("MyXformMat", m);
        // "MyXformMat" must be exactly the same string as what is defined in the shader of the material

        // for side-by-side comparison purposes
        DefaultCylinder.transform.localScale = transform.localScale;
        DefaultCylinder.transform.localPosition = transform.localPosition + new Vector3(2, 0, 2);
        DefaultCylinder.transform.localRotation = transform.localRotation;
	}

    void IncrementXform()
    {
        // rotation 
        if (ShowRotation)
        {
            Quaternion q = Quaternion.AngleAxis(kRotateDelta * Time.fixedDeltaTime, transform.up);
            transform.localRotation = q * transform.localRotation;
        }

        if (ShowScale)
        {
            if ((transform.localScale.x > kMaxSize) || (transform.localScale.x <= kMinSize))
                IncSign *= -1;
            transform.localScale += IncSign * Time.fixedDeltaTime * kScaleDelta;
        }
    }
}
