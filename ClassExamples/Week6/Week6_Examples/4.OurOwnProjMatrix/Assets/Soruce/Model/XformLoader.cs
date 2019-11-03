using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    private Material mMaterial;  // keep a reference for convenience/efficiency sake!
    public Vector3 Pivot = Vector3.zero;

    public Color MyColor = Color.black;
   
	// Use this for initialization
	void Start () {
        mMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        Matrix4x4 m = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 pm = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 ipm = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        
        m = pm * m * ipm;    

        mMaterial.SetMatrix("MyXformMat", m);
        // "MyXformMat" must be exactly the same string as what is defined in the shader of the material
        mMaterial.SetColor("MyColor", MyColor);
	}
}
