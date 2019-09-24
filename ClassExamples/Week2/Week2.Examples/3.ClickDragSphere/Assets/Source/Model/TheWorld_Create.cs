using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour  {

    public void CreateBallAt(Vector3 pos)
    {
        ReleaseSelected();

        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        mSelectedSphere = g.AddComponent<BallBehavior>();
        mSelectedSphere.transform.localPosition = pos;
    }

    public void ResizeCreatedTo(Vector3 pos)
    {
        Debug.Assert(mSelectedSphere != null);
        mSelectedSphere.SetSize(ref pos);
    }

    public void EnableCreatedMotion()
    {
        if (mSelectedSphere != null)
            mSelectedSphere.EnableMotion();
    }
}