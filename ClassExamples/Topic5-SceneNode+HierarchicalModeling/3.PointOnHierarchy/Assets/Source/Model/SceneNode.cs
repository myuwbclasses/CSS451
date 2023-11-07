using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;
    public List<SceneNode> ChildrenList;

    public Transform AxisFrame = null;
    public Vector3 kDefaultTreeTip = new Vector3(0.19f, 12.69f, 3.88f);
    public bool UseUnity = false;

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
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * orgT * trs;

        // propagate to all children
        foreach (SceneNode child in ChildrenList)
            child.CompositeXform(ref mCombinedParentXform);
        
        // disseminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
            p.LoadShaderMatrix(ref mCombinedParentXform);

        // Compute AxisFrame 
        if (AxisFrame != null)
        {
            AxisFrame.localPosition = mCombinedParentXform.MultiplyPoint(kDefaultTreeTip);

            // 
            // What is going on in the next two lines of code?
            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;

            if (UseUnity) {
                AxisFrame.localRotation = Quaternion.LookRotation(forward, up);
            } else {
                // First align up direction, remember that the default AxisFrame.up is simply the y-axis
                float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
                Vector3 axis = Vector3.Cross(Vector3.up, up);
                AxisFrame.localRotation = Quaternion.AngleAxis(angle, axis);

                // Now, align the forward axis
                angle = Mathf.Acos(Vector3.Dot(AxisFrame.transform.forward, forward)) * Mathf.Rad2Deg;
                axis = Vector3.Cross(AxisFrame.transform.forward, forward);
                AxisFrame.localRotation = Quaternion.AngleAxis(angle, axis) * AxisFrame.localRotation;
            }
        }
    }
}