using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATriangle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        int[] t = theMesh.GetTriangles(0);
        Debug.Log("Triangle:" + t.Length + " " + t[0] + " " + t[1] + " " + t[2]);
        Debug.Log("Triangle:" + t.Length + " " + t[3] + " " + t[4] + " " + t[5]);
        Vector3[] v = theMesh.vertices;
        Debug.Log("Vertices:" + v.Length + " " + v[0]);
        Debug.Log("Vertices:" + v.Length + " " + v[1]);
        Debug.Log("Vertices:" + v.Length + " " + v[2]);

        theMesh.Clear();    // delete whatever is there!!

        v = new Vector3[3];     // Allocate new ones!
        t = new int[3];


        v[0] = new Vector3(-0.5f, 0, -0.5f);
        v[1] = new Vector3(0.5f, 0, 0.5f);
        v[2] = new Vector3(0.5f, 0, -0.5f);

        t[0] = 0;   // t array is always multiples of 3
        t[1] = 1;   //  WATCH for the default culling!! CCW by default is culled!
        t[2] = 2;
        

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = null;


        /*
            NEVER do this:
                mesh.vertices = new Vector3[3];
                mesh.vertices[0] = new Vector3(0, 1, 0);
            
            The above DOES NOT WORK!! YOU MUST FIRST allocate and set the arrays BEFORE
            assigning to mesh.WHATEVER

            I believe, during the assignment, mesh initialize stuff!
          */
    }

    // Update is called once per frame
    void Update () {
		
	}
}
