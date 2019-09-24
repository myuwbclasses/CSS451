using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    public enum TransformMode
    {
        Translation = 0,
        Scale = 1,
        Rotate = 2,
        None = 3
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

        switch (mode)
        {
            case TransformMode.Translation:
                Vector3 p = transform.localPosition;
                m[12] = p.x;    // col-3, row-0
                m[13] = p.y;    // col-3, row-1
                m[14] = p.z;    // col-3, row-2
                break;
            case TransformMode.Scale:
                Vector3 s = transform.localScale;
                m[0] = s.x; // col-0, row-0
                m[5] = s.y; // col-1, row-1
                m[10] = s.z; // col-2, row-2
                break;
            case TransformMode.Rotate:
                Quaternion q = transform.localRotation;
                m.SetTRS(Vector3.zero, q, Vector3.one);
                break;
        }        

        // identical effct:
        //      m = Matrix4x4.TRS(transform.localPosition, Quaternion.identity, Vector3.one);

        mMaterial.SetMatrix("MyXformMat", m);
            // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
	}
}
