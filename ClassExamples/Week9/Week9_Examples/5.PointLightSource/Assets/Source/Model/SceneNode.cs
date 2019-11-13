using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Transform AxisFrame;
    public Vector3 Pivot = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;

    const float kAxisFrameSize = 5f;

    void Awake()
    {
        UnSelect();
    }

	// Use this for initialization
	protected void Start () {
        Debug.Assert(AxisFrame != null);
        InitializeSceneNode();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetToSelect() { AxisFrame.gameObject.SetActive(true); }
    public void UnSelect() { AxisFrame.gameObject.SetActive(false); }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // tipPos: is the origin of this scene node
    // topDir: is the y-direction of this node
    public void CompositeXform(ref Matrix4x4 parentXform, out Vector3 tipPos, out Vector3 tipDir)
    {
        Matrix4x4 pivot = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);  // Pivot translation
        Matrix4x4 invPivot = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);  // inv Pivot
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * pivot * trs;
        tipPos = mCombinedParentXform.GetColumn(3);
        tipDir = mCombinedParentXform.MultiplyVector(Vector3.up);

        // let's decompose the combined matrix into R, and S
        Vector3 c0 = mCombinedParentXform.GetColumn(0);
        Vector3 c1 = mCombinedParentXform.GetColumn(1);
        Vector3 c2 = mCombinedParentXform.GetColumn(2);
        Vector3 s = new Vector3(c0.magnitude, c1.magnitude, c2.magnitude);
        Matrix4x4 r = Matrix4x4.identity;
        c0 /= s.x;  // normalize the columns
        c1 /= s.y;
        c2 /= s.z;
        r.SetColumn(0, c0);
        r.SetColumn(1, c1);
        r.SetColumn(2, c2);
        Quaternion q = Quaternion.LookRotation(c2, c1); // creates a rotation matrix with c2-Forward, c1-up

        AxisFrame.transform.localPosition = tipPos;  // our location is Pivot 
        AxisFrame.transform.localScale = s * kAxisFrameSize;
        AxisFrame.transform.localRotation = q;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform, out tipPos, out tipDir);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }

    }
}