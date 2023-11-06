using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        Vector3[] v = new Vector3[9];   // 2x2 mesh needs 3x3 vertices
        int[] t = new int[8*3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        Vector3[] n = new Vector3[9];   // MUST be the same as number of vertices
        Vector2[] uv = new Vector2[9];


        v[0] = new Vector3(-1, 0, -1);
        v[1] = new Vector3( 0, 0, -1);
        v[2] = new Vector3( 1, 0, -1);

        v[3] = new Vector3(-1, 0, 0);
        v[4] = new Vector3( 0, 0, 0);
        v[5] = new Vector3( 1, 0, 0);

        v[6] = new Vector3(-1, 0, 1);
        v[7] = new Vector3( 0, 0, 1);
        v[8] = new Vector3( 1, 0, 1);

        // UV
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0.5f, 0);
        uv[2] = new Vector2(1, 0);

        uv[3] = new Vector2(0, 0.5f);
        uv[4] = new Vector2(0.5f, 0.5f);
        uv[5] = new Vector2(1, 0.5f);

        uv[6] = new Vector2(0, 1);
        uv[7] = new Vector2(0.5f, 1);
        uv[8] = new Vector2(1, 1);

        n[0] = new Vector3(0, 1, 0);
        n[1] = new Vector3(0, 1, 0);
        n[2] = new Vector3(0, 1, 0);
        n[3] = new Vector3(0, 1, 0);
        n[4] = new Vector3(0, 1, 0);
        n[5] = new Vector3(0, 1, 0);
        n[6] = new Vector3(0, 1, 0);
        n[7] = new Vector3(0, 1, 0);
        n[8] = new Vector3(0, 1, 0);

        // First triangle
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;

        // Second UV (create a slight offset to see the effect!)
        Vector2 offset = new Vector2(0.5f, 0.8f);
        for (int i = 0; i < uv.Length; i++)
            uv[i] += offset;
        theMesh.uv2 = uv;

        InitControllers(v);
        InitNormals(v, n);
    }

    // Update is called once per frame
    void Update () {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        for (int i = 0; i<mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, n);

        theMesh.vertices = v;
        theMesh.normals = n;
	}
}
