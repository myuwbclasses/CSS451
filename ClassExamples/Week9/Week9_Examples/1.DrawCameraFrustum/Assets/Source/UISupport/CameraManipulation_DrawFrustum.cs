using UnityEngine;
using System.Collections;

public partial class CameraManipulation : MonoBehaviour
{
    Rectangle mNearPlane, mFarPlane;
    LineSegment mLL, mLR, mUL, mUR;
    LineSegment mSight;

    /*  --> if we want to avoid drawing the frustum of the currently rendering camera
     *  Can potentially block its own view (when n is small)
     */
     /*
    void OnPreRender()
    {
        if (Camera.current == GetComponent<Camera>())
            ShowFrustum(false);
    } */

    void InitializeFrustum()
    {
        mNearPlane = Rectangle.CreateRectangle();
        mFarPlane = Rectangle.CreateRectangle();
        mLL = LineSegment.CreateLineSegment();
        mLR = LineSegment.CreateLineSegment();
        mUL = LineSegment.CreateLineSegment();
        mUR = LineSegment.CreateLineSegment();
        mSight = LineSegment.CreateLineSegment();
        UpdateFrustumPosition();
    }

    void UpdateFrustumPosition()
    {
        ShowFrustum(DrawFrustum);
        if (!DrawFrustum)
            return;

        Vector3 eye = transform.localPosition;
        Camera c = GetComponent<Camera>();
        float tanFOV = Mathf.Tan(Mathf.Deg2Rad * 0.5f * c.fieldOfView);
        // near plane dimension
        float n = c.nearClipPlane;
        float nearPlaneHeight = 2f * n * tanFOV;
        float nearPlaneWidth = c.aspect * nearPlaneHeight;
        Vector3 nearPlaneCenter = eye + n * transform.forward;
        mNearPlane.UpdateRectangle(nearPlaneCenter, transform.right, nearPlaneWidth, transform.up, nearPlaneHeight);

        // far plane dimension
        float f = c.farClipPlane;
        float farPlaneHeight = 2f * f * tanFOV;
        float farPlaneWidth = c.aspect * farPlaneHeight;
        Vector3 farPlaneCenter = eye + f * transform.forward;
        mFarPlane.UpdateRectangle(farPlaneCenter, transform.right, farPlaneWidth, transform.up, farPlaneHeight);

        mLL.SetEndPoints(mNearPlane.GetLL(), mFarPlane.GetLL());
        mLR.SetEndPoints(mNearPlane.GetLR(), mFarPlane.GetLR());
        mUL.SetEndPoints(mNearPlane.GetUL(), mFarPlane.GetUL());
        mUR.SetEndPoints(mNearPlane.GetUR(), mFarPlane.GetUR());
        mSight.SetEndPoints(eye, LookAt.localPosition);
    }

    void ShowFrustum(bool show)
    {
        mNearPlane.Show(show);
        mFarPlane.Show(show);
        mLL.gameObject.SetActive(show);
        mLR.gameObject.SetActive(show);
        mUL.gameObject.SetActive(show);
        mUR.gameObject.SetActive(show);
        mSight.gameObject.SetActive(show);
    }
}
