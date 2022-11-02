using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VERY expensive!!
// [ExecuteInEditMode]
public class XformLoader : MonoBehaviour {

    public Color Color = Vector4.one;
    public Vector3 PivotOffset = Vector3.zero;   // Pivot X/Y/Z
    public bool ShowRotation = false;
    public bool ShowScale = false;

    public XformLoader Parent = null;

    public Transform DrawPivot;
    private Material mMaterial;  // keep a reference for convenience/efficiency sake!

    // to support slow rotation about the x-axis
    private const float kRotateDelta = 45; // per second
    private const float kRotateSize = 80;
    private int RotateSign = 1;
    private int RotateCount = 1;
    private Vector3 kScaleDelta = new Vector3(0.1f, 0.1f, 0.1f); // per second
    private const float kMaxSize = 5;
    private const float kMinSize = 0.2f;
    private float IncSign = 1;

   
	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
        mMaterial.SetColor("MyColor", Color);
	}
	
	// Update is called once per frame
	void Update () {

        IncrementXform();

        Matrix4x4 m = ComputeXform();
        Vector3 pivotPosition = transform.localPosition + PivotOffset;
        if (Parent != null)
        {
            Matrix4x4 p = Parent.ComputeXform();
            m = p * m; // our own first, then the parent

            // Since Pivot is NOT a 451Material, we must compute its position in the CPU
            pivotPosition = p.MultiplyPoint(pivotPosition);
        }

        DrawPivot.localPosition = pivotPosition;
        DrawPivot.localRotation = transform.localRotation;
                //  Note: this rotation is wrong, we are not considering the parent's rotation here!
                //        the matrix operator P: contains parent rotation information, but
                //        at this point we don't know how to get at it (we will learn about this soon)

        mMaterial.SetMatrix("MyXformMat", m);
            // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
	}
    
    public Matrix4x4 ComputeXform()
    {
        Matrix4x4 m = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 pm = Matrix4x4.TRS(PivotOffset, Quaternion.identity, Vector3.one);  // PivotOffset Translation
        Matrix4x4 ipm = Matrix4x4.TRS(-PivotOffset, Quaternion.identity, Vector3.one);
        m = pm * m * ipm;
        return m;
    }

    void IncrementXform()
    {
        // rotation 
        if (ShowRotation)
        {
            float angle = RotateSign * kRotateDelta * Time.fixedDeltaTime;
            Quaternion q = Quaternion.AngleAxis(angle, transform.right);
            transform.localRotation = q * transform.localRotation;
            RotateCount += RotateSign;
            if (Mathf.Abs(RotateCount) > kRotateSize)
            {
                RotateSign *= -1;
            }
        }

        if (ShowScale)
        {
            if ((transform.localScale.x > kMaxSize) || (transform.localScale.x <= kMinSize))
                IncSign *= -1;
            transform.localScale += IncSign * Time.fixedDeltaTime * kScaleDelta;
        }
    }
}
