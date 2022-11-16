using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// **WARNING***
//  Assume the list of children does not change (not even the order) at run time

public class ShowInView_NDC_Space : MonoBehaviour
{
    bool ShowNDCSpace = false;
    public Camera mTheView = null;

    private GameObject mEyeSpaceRoot = null;

    GameObject[] mTheList;
    Mesh[]  mOrgMesh;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(mTheView != null);
        mTheList = new GameObject[transform.childCount];
        mOrgMesh = new Mesh[transform.childCount];

        mEyeSpaceRoot = new GameObject();
        for (int i = 0; i<transform.childCount; i++) {
            mOrgMesh[i] = Instantiate(transform.GetChild(i).GetComponent<MeshFilter>().mesh);
            mTheList[i] = Instantiate(transform.GetChild(i).gameObject);
            mTheList[i].transform.localPosition = Vector3.zero;
            mTheList[i].transform.localScale = Vector3.one;
            mTheList[i].transform.localRotation = Quaternion.identity;
            mTheList[i].transform.SetParent(mEyeSpaceRoot.transform);
        }
    }

    public void SetToNDCSpace(bool b) { ShowNDCSpace = b; }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<transform.childCount; i++) {    
            // Transform mTheObject to world, then to view space of mTheView
            Matrix4x4 m = mTheView.worldToCameraMatrix * transform.GetChild(i).localToWorldMatrix;

            /*
            This is effective transform to the ViewSpace, but, we will explicitly compute the transformation in transformMesh

            Vector3 x = m.GetColumn(0);
            Vector3 y = m.GetColumn(1);
            Vector3 z = m.GetColumn(2);
            Vector3 size = new Vector3(x.magnitude, y.magnitude, z.magnitude);
            // set my transform by the concatenation of 
            // the object and the view
            mTheList[i].transform.localPosition = m.GetColumn(3);
            mTheList[i].transform.localScale = size;
            mTheList[i].transform.localRotation = Quaternion.LookRotation(z/size.z, y/size.y);
            */
            
            if (ShowNDCSpace) {
                m = mTheView.projectionMatrix * m;
            } 
            transformMesh(ref m, i);
        }                 
    }

    void transformMesh(ref Matrix4x4 m, int meshIndex) {
        Mesh dstMesh = mTheList[meshIndex].gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] v = dstMesh.vertices;
        Vector3[] n = dstMesh.normals;
        for (int i=0; i<mOrgMesh[meshIndex].vertexCount; i++) {
            v[i] = m.MultiplyPoint(mOrgMesh[meshIndex].vertices[i]);
            n[i] = m.MultiplyVector(mOrgMesh[meshIndex].normals[i]);
        }
        dstMesh.vertices = v;
        dstMesh.normals = n;
    }
}