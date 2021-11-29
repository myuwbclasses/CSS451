using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    private Matrix4x4 mParentXform;
    
    
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;

	// Use this for initialization
	protected void Start () {
        InitializeSceneNode();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
        mParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mParentXform = parentXform;
        mCombinedParentXform = parentXform * orgT * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }
    }

    public void OrbitAroundWorldY(float deltaDegree) {
        Quaternion qy = Quaternion.AngleAxis(deltaDegree, Vector3.up);
        transform.localRotation = qy * transform.localRotation;
    }

    public void AlignUpWith(Vector3 dir) {
        // rotation required to rotate parent's up to dir
        Quaternion q = Quaternion.FromToRotation(mParentXform.GetColumn(1), dir);

        // qp is parent rotation
        Quaternion qp = Quaternion.LookRotation(mParentXform.GetColumn(2), mParentXform.GetColumn(1));

        // q is rotation form qp's y 
        // so, q is applied after qp
        //      rotation needed is = q * qp 
        // But this sceneNode's rotation is applied _BEFORE_ parent rotation, 
        //     so we need to compute qa where:
        //      q * qp = qp * qa
        // multiply both side by qp-inverset
        //      qp=Inv * q * qp = qa
        // So ...
        Quaternion qa = Quaternion.Inverse(qp) * q * qp;
        transform.localRotation = qa;
    }

    public void RotateUpTowardsBy(Vector3 dir, float delta) {
        Vector3 pUp = mParentXform.GetColumn(1).normalized;  // parent's up
        Vector3 rAxis = Vector3.Cross(pUp, dir);  // rotation axis
        float rotDegree = Mathf.Acos(Vector3.Dot(pUp, dir.normalized)) * Mathf.Rad2Deg; // angle to rotate
        Quaternion q = Quaternion.AngleAxis(delta*rotDegree, rAxis);  // rotate by delta of the actual angle
        // qp is parent rotation
        Quaternion qp = Quaternion.LookRotation(mParentXform.GetColumn(2), mParentXform.GetColumn(1));
        transform.localRotation = Quaternion.Inverse(qp) * q * qp; // same as AlignUpWith();
    }

    public void SetAxisFrame(Transform t) { 
        t.localPosition = mCombinedParentXform.GetColumn(3);
        // Vector3 x = mCombinedParentXform.GetColumn(0).normalized;
        Vector3 y = mCombinedParentXform.GetColumn(1).normalized;
        Vector3 z = mCombinedParentXform.GetColumn(2).normalized;
        t.localRotation = Quaternion.LookRotation(z, y);
    }
}