using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour  {

    BallBehavior mSelectedSphere = null;
    
    public void SetSelected(ref GameObject g)
    {
        ReleaseSelected();
        mSelectedSphere = g.GetComponent<BallBehavior>();
        Debug.Assert(mSelectedSphere != null);
        mSelectedSphere.SetAsSelected();
    }

    public void ReleaseSelected()
    {
        if (mSelectedSphere != null)
        {
            mSelectedSphere.ReleaseSelection();
            mSelectedSphere = null;
        }
    }

    public void SetSelectedPosition(float x, float z)
    {
        if (mSelectedSphere != null)
            mSelectedSphere.SetPosition(x, z);
    }

    public Vector3 GetSelectedPosition()
    {
        if (mSelectedSphere != null)
            return mSelectedSphere.transform.localPosition;
        else
            return Vector3.zero;
    }

    public bool HasSelected() { return mSelectedSphere != null; }
}