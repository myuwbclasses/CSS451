using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumInSpace : Frustum
{
    public Camera mTheView;
    bool ShowInNDC = false;

    public void SetToNDCSpace(bool b) { ShowInNDC = b; }

    protected override void GetCamera()
    {
        mTheCamera = mTheView;
    }

    override protected Vector3 eyePos() {
        return Vector3.zero;
    }
    override protected Vector3 rightDir() {
        return Vector3.right;
    }
    override protected Vector3 upDir() {
        return Vector3.up;
    }
    override protected Vector3 forwardDir() {
        return -Vector3.forward;
    }

    new protected void Start() {
        base.Start();
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    protected override void UpdateFrustum()
    {
        if (ShowInNDC) {
            mSight.gameObject.SetActive(false);
            mNearPlane.UpdateRectangle(new Vector3(0, 0, 0), 
                        rightDir(), 2.0f, 
                        upDir(), 2.0f);
            mFarPlane.UpdateRectangle(new Vector3(0, 0, 1),
                        rightDir(), 2.0f,
                        upDir(), 2.0f);
            ConnectNearFar();
        } else {
            mSight.gameObject.SetActive(true);
            base.UpdateFrustum();
        }
    }
}
