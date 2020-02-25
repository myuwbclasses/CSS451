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

    public GameObject GetSelectedObject()
    {
        if (mSelectedSphere != null)
            return mSelectedSphere.transform.gameObject;
        else
            return null;
    }
}