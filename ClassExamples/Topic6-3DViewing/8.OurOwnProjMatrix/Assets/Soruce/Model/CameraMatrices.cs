using UnityEngine;
using System.Collections;

public class CameraMatrices: MonoBehaviour {
    
    public enum ViewMatrixMode {
        UseTransformRotate = 0,
        SetMatrixCol = 1,
        SetMatrixRow = 2,
        ComputeFromScratch = 3,
        ViewMatrixWrong = 4  // just to show ourselves we are actually doing something

    };

    public enum ProjectionMatrix
    {
        Perspective = 0,
        Orthographics = 1
    };

    public ViewMatrixMode ViewMatrixCompute = ViewMatrixMode.UseTransformRotate;
    public ProjectionMatrix ProjectionMode = ProjectionMatrix.Perspective;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnPreRender()
    {
        // Debug.Log("OnPreRender:" + name);
        Matrix4x4 r = Matrix4x4.identity;

        switch (ViewMatrixCompute)
        {
            case ViewMatrixMode.UseTransformRotate:
                r = Matrix4x4.TRS(Vector3.zero, transform.localRotation, Vector3.one);
                r.SetColumn(2, -r.GetColumn(2));
                r = r.inverse;
                break;

            case ViewMatrixMode.SetMatrixCol:
                // set the columns, and take the inverse
                r.SetColumn(0, transform.right);
                r.SetColumn(1, transform.up);
                r.SetColumn(2, -transform.forward);
                r = r.inverse;  // we need the inverse of this matrix
                break;

            case ViewMatrixMode.SetMatrixRow:
                // Set the rows, for rotation matrices, this is identical to setColumn followed by inverse
                r.SetRow(0, transform.right);
                r.SetRow(1, transform.up);
                r.SetRow(2, -transform.forward);
                break;

            case ViewMatrixMode.ComputeFromScratch:
                Vector3 V = -transform.forward;  // -ve Z is the viewing direction
                Vector3 U = transform.up; // this is the up vector
                Vector3 W = Vector3.Cross(V, U);
                U = Vector3.Cross(W, V);
                r.SetRow(0, W.normalized);
                r.SetRow(1, U.normalized);
                r.SetRow(2, V.normalized);
                break;

            case ViewMatrixMode.ViewMatrixWrong:
                // Alternative: this is to show we are doing something
                break;
        }
        Matrix4x4 t = Matrix4x4.TRS(-transform.position, Quaternion.identity, Vector3.one);
        
        Matrix4x4 u = r * t;
        Shader.SetGlobalMatrix("CameraViewMatrix", u);

        Camera c = GetComponent<Camera>();
        Matrix4x4 p = Matrix4x4.identity;
        if (ProjectionMode == ProjectionMatrix.Perspective)
        {
            p = Matrix4x4.Perspective(c.fieldOfView, c.aspect, c.nearClipPlane, c.farClipPlane);

            // depending on if you are working with GLSL or HLSL, this may not work!
        }  else
        {
            // remember Width/Height = aspect
            float height = 10;
            float width = height * c.aspect;
            p = Matrix4x4.Ortho(-width, width, -height, height, c.nearClipPlane, c.farClipPlane);
        }
        Shader.SetGlobalMatrix("CameraProjMatrix", p);
    }
        
}
