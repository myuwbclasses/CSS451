using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {
    public Transform TreeXform;      // Xform of the Tree
    public GameObject X, Y, Z; // these are the rotated X/Y/Z axis 

    private void Start()
    {
        Debug.Assert(TreeXform != null);
        Debug.Assert(X != null);
        Debug.Assert(Y != null);
        Debug.Assert(Z != null);
    }

    private void Update()
    {
        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, TreeXform.localRotation, Vector3.one);  // or Matrix4x4.Rotate(TreeXform.localRotation)
        // Note: the end result is transforming a Quaternion into a matrix
        //       this matrix is _very_ interesting!!
        // Ortho-Normal!!

        DrawCylinderAsVector(transform.localPosition, m.GetColumn(0), X);
        DrawCylinderAsVector(transform.localPosition, m.GetColumn(1), Y);
        DrawCylinderAsVector(transform.localPosition, m.GetColumn(2), Z);
    }

    private void DrawCylinderAsVector(Vector3 p, Vector3 v, GameObject g)  // assume g is a cylinder
    {
        // rotate the cylinder to orientate along V
        Quaternion q = Quaternion.FromToRotation(Vector3.up, v);
        g.transform.localPosition = p + 1.75f * v;  // moving in the v direction by unit of 1.75
        g.transform.localRotation = q;
    }
}
