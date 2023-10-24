using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]  // run in editor mode
public class XformLoader : MonoBehaviour {

    public enum TransformMode
    {
        Translation = 0,
        Scale = 1,
        Rotate = 2,
        TRS = 3,
        None = 4
    };

    private Material mMaterial;  // keep a reference for convenience/efficiency sake!
    public TransformMode mode; // 0:T, 1:R, 2:S

	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        // let's use the translation from GameObject and construct a matrix based on that
        Matrix4x4 m = Matrix4x4.identity;  // column major, column first ...
        Matrix4x4 T = Matrix4x4.identity;
        Matrix4x4 R = Matrix4x4.identity;
        Matrix4x4 S = Matrix4x4.identity;
        Vector3 p = transform.localPosition;
        Vector3 s = transform.localScale;
        Quaternion q = transform.localRotation;
        switch (mode)
        {
            case TransformMode.Translation:
                
                m[12] = p.x;    // col-3, row-0
                m[13] = p.y;    // col-3, row-1
                m[14] = p.z;    // col-3, row-2
                //Debug.Log("Compute Translation");
                break;
            case TransformMode.Scale:
                
                m[0] = s.x; // col-0, row-0
                m[5] = s.y; // col-1, row-1
                m[10] = s.z; // col-2, row-2
                //Debug.Log("Compute  Scale");
                break;
            case TransformMode.Rotate:
                
                m.SetTRS(Vector3.zero, q, Vector3.one);
                //Debug.Log("Compute Rotate");
                break;
            case TransformMode.TRS:
            {
                // m = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
                
                T[12] = p.x;    // col-3, row-0
                T[13] = p.y;    // col-3, row-1
                T[14] = p.z;    // col-3, row-2

                
                S[0] = s.x; // col-0, row-0
                S[5] = s.y; // col-1, row-1
                S[10] = s.z; // col-2, row-2

                
                R = Matrix4x4.Rotate(q);

                m = T * R * S;
                //Debug.Log("Compute TRS");
            }
                break;
        }        
        
        mMaterial.SetMatrix("MyXformMat", m);
            // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
	}
}

