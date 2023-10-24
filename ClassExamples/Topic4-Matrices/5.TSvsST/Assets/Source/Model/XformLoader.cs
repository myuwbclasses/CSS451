using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    public enum STOrTSMode
    {
        ScaleThenTranslate = 0,
        TranslateThenScale = 1
    };

    private Material mMaterial;  // keep a reference for convenience/efficiency sake!
    public STOrTSMode mode; // 

	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        // let's use the translation from GameObject and construct a matrix based on that
        Vector3 p = transform.localPosition;
        Vector3 s = transform.localScale;

        Matrix4x4 sm = Matrix4x4.Scale(s);
        Matrix4x4 tm = Matrix4x4.identity;
        tm[12] = p.x;    // col-3, row-0
        tm[13] = p.y;    // col-3, row-1
        tm[14] = p.z;    // col-3, row-2

        Matrix4x4 cm = Matrix4x4.identity; // concatenated result

        switch (mode)
        {
            case STOrTSMode.ScaleThenTranslate:
                cm = tm * sm;  // this is sm FIRST and then tm
                break;
            case STOrTSMode.TranslateThenScale:
                cm = sm * tm;
                break;
        }        

        mMaterial.SetMatrix("MyXformMat", cm);
            // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
	}
}
