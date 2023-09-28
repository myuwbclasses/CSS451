using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {
    ScalableSphere mSelectedSphere = null;

    public void SelectObjectAt(GameObject obj, Vector3 p)
    {
        // 1. unselect the current
        UnSelectCurrent();

        if (obj.name == "CreationPlane")
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mSelectedSphere = o.AddComponent<ScalableSphere>();
            mSelectedSphere.SetSpherePosition(p);
        }
        else {
            mSelectedSphere = obj.GetComponent<ScalableSphere>();
            Debug.Assert(mSelectedSphere != null);
        }
        SetSelectedColor();
    }

    public void SetSelelectedRadius(float r)
    {
        if (mSelectedSphere != null)
            mSelectedSphere.SetSize(r);
    }

    public float GetSelectedRadius() {
        return mSelectedSphere.transform.localScale.x; // assume x,y,z are the same
    }

    private void UnSelectCurrent()
    {
        if (mSelectedSphere != null)
        {
            mSelectedSphere.GetComponent<Renderer>().material.color = Color.blue;
            mSelectedSphere = null;
        }
    }

    private void SetSelectedColor()
    {
        Debug.Assert(mSelectedSphere != null);
        mSelectedSphere.GetComponent<Renderer>().material.color = Color.red;
    }
}