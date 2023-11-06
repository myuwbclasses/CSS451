using UnityEngine;
using System.Collections;

public partial class Frustum : MonoBehaviour
{
    public bool mDraw = true;
    private float mFrustumScale = 0.1f;
    protected Camera mTheCamera;

    protected Rectangle mNearPlane, mFarPlane;
    protected LineSegment mLL, mLR, mUL, mUR;
    protected LineSegment mSight;

    protected void Start()
    {
        GetCamera();
        mNearPlane = Rectangle.CreateRectangle();
        mFarPlane = Rectangle.CreateRectangle();
        mLL = LineSegment.CreateLineSegment();
        mLR = LineSegment.CreateLineSegment();
        mUL = LineSegment.CreateLineSegment();
        mUR = LineSegment.CreateLineSegment();
        mSight = LineSegment.CreateLineSegment();
        SetFrustumScale();
    }

    virtual protected void  GetCamera() {
        mTheCamera = GetComponent<Camera>();
    }

    virtual protected Vector3 eyePos() {
        return mTheCamera.transform.localPosition;
    }
    virtual protected Vector3 rightDir() {
        return mTheCamera.transform.right;
    }
    virtual protected Vector3 upDir() {
        return mTheCamera.transform.up;
    }
    virtual protected Vector3 forwardDir() {
        return mTheCamera.transform.forward;
    }

    protected void Update()
    {
        ShowFrustum(mDraw);
        if (!mDraw)
            return;
        UpdateFrustum();
    }

    virtual protected void UpdateFrustum() {
        Vector3 eye = eyePos();
        float tanFOV = Mathf.Tan(Mathf.Deg2Rad * 0.5f * mTheCamera.fieldOfView);
        // near plane dimension
        float n = mTheCamera.nearClipPlane;
        float nearPlaneHeight = 2f * n * tanFOV;
        float nearPlaneWidth = mTheCamera.aspect * nearPlaneHeight;
        Vector3 nearPlaneCenter = eye + n * forwardDir();
        mNearPlane.UpdateRectangle(nearPlaneCenter, 
                        rightDir(), nearPlaneWidth, 
                        upDir(), nearPlaneHeight);

        // far plane dimension
        float f = mTheCamera.farClipPlane;
        float farPlaneHeight = 2f * f * tanFOV;
        float farPlaneWidth = mTheCamera.aspect * farPlaneHeight;
        Vector3 farPlaneCenter = eye + f * forwardDir();
        mFarPlane.UpdateRectangle(farPlaneCenter, 
                    rightDir(), farPlaneWidth, 
                    upDir(), farPlaneHeight);

        mSight.SetEndPoints(eye, farPlaneCenter);
        ConnectNearFar();
    }

    protected void ConnectNearFar() {
        mLL.SetEndPoints(mNearPlane.GetLL(), mFarPlane.GetLL());
        mLR.SetEndPoints(mNearPlane.GetLR(), mFarPlane.GetLR());
        mUL.SetEndPoints(mNearPlane.GetUL(), mFarPlane.GetUL());
        mUR.SetEndPoints(mNearPlane.GetUR(), mFarPlane.GetUR());
    }

    protected void ShowFrustum(bool show)
    {
        mNearPlane.Show(show);
        mFarPlane.Show(show);
        mLL.gameObject.SetActive(show);
        mLR.gameObject.SetActive(show);
        mUL.gameObject.SetActive(show);
        mUR.gameObject.SetActive(show);
        mSight.gameObject.SetActive(show);
    }

    void SetFrustumScale() {
        mNearPlane.SetScale(mFrustumScale);
        mFarPlane.SetScale(mFrustumScale);
        mLL.SetWidth(mFrustumScale);
        mLR.SetWidth(mFrustumScale);
        mUL.SetWidth(mFrustumScale);
        mUR.SetWidth(mFrustumScale);
        mSight.SetWidth(mFrustumScale);
    }
}
