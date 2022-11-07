using UnityEngine;
using System.Collections;

public partial class CameraManipulation : MonoBehaviour
{
    GameObject[] mIndicators; // spheres to show the mouse click position
    LineSegment mSelectionRay;
    void InitShowMouseClick()
    {
        mIndicators = new GameObject[10];
        for (int i = 0; i<10; i++)
        {
            mIndicators[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mIndicators[i].GetComponent<Renderer>().material.color = Color.yellow;
            mIndicators[i].transform.localScale = new Vector3(3, 3, 3);
        }

        mSelectionRay = LineSegment.CreateLineSegment();
        mSelectionRay.gameObject.GetComponent<Renderer>().material.color = Color.red;

        ShowIndicators(false);
    }

    void ShowIndicators(bool show)
    {
        foreach (GameObject g in mIndicators)
            g.SetActive(show);
        mSelectionRay.gameObject.SetActive(show);
    }

    Vector3 MyScreenToViewport(Vector3 screenPt)
    {
        Camera c = GetComponent<Camera>();
        Vector3 viewportPt = Vector3.zero;
        viewportPt.x = (screenPt.x - c.pixelRect.xMin) / c.pixelWidth;
        viewportPt.y = (screenPt.y - c.pixelRect.yMin) / c.pixelHeight;
        return viewportPt;
    }

    void ShowScreenToWorld(Vector3 screenPt)
    {
        Vector3 viewportPt;
        viewportPt = MyScreenToViewport(screenPt);
        if ((viewportPt.x > 0) && (viewportPt.x < 1.0) && (viewportPt.y > 0) && (viewportPt.y < 1.0))
        {
            Vector3 worldPt;
            Camera c = GetComponent<Camera>();
            ShowIndicators(true);

            // compute worldPt (on near plane) by transforming from viewport coordinate (a percentage) 
            // to an actual position on the near plane (porportionally) scalled.
            worldPt = mNearPlane.GetLL() + viewportPt.x * mNearPlane.GetWidth() * transform.right +
                                           viewportPt.y * mNearPlane.GetHeight() * transform.up;
            // now compute points along the vector from eye to worldPt
            Vector3 eye = transform.localPosition;
            Vector3 dir = (worldPt - eye).normalized;
            float dist = (c.farClipPlane - c.nearClipPlane) / (mIndicators.Length - 1);
            for (int i = 0; i < mIndicators.Length; i++)
            {
                mIndicators[i].transform.localPosition = worldPt + i * dist * dir;
                mIndicators[i].gameObject.SetActive(true);
            }
            mSelectionRay.SetEndPoints(eye, mIndicators[9].transform.localPosition);
        }
    }
}
