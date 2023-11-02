using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    public Transform ShowPivotPosition;
    public Vector3 PivotPosition;
        // Tree's width is about 1.1 or 1.2 in X/Z
    public bool ShowRotation = false;
    public bool ShowScale = false;

    private Material mMaterial;  // keep a reference for convenience/efficiency sake!

    // to support slow rotation about the y-axis
    private const float kRotateDelta = 45; // per second
    private Vector3 kScaleDelta = new Vector3(0.2f, 0.2f, 0.2f); // per second
    private const float kMaxSize = 5;
    private const float kMinSize = 0.2f;
    private float IncSign = 1;

   
	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

        // Pivot is an offset in the ObjectSpace
        ShowPivotPosition.localPosition = transform.localPosition + PivotPosition;

        IncrementXform();
        Matrix4x4 m = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        Matrix4x4 ipm = Matrix4x4.Translate(-PivotPosition);
        Matrix4x4 pm = Matrix4x4.Translate(PivotPosition);

        m = pm * m * ipm;    

        mMaterial.SetMatrix("MyXformMat", m);
            // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
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
