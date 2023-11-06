using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour {
    LineSegment mTop, mBottom, mLeft, mRight;
    Vector3 mCenter, mLL, mLR, mUL, mUR;  // center and upper/lower left/right

    public static Rectangle CreateRectangle()
    {
        return new GameObject().AddComponent<Rectangle>();
    }

    const float kLineWidth = 1.2f;
    void Awake()
    {
        mTop = LineSegment.CreateLineSegment();
        mBottom = LineSegment.CreateLineSegment();
        mLeft = LineSegment.CreateLineSegment();
        mRight = LineSegment.CreateLineSegment();

        mTop.SetWidth(kLineWidth);
        mBottom.SetWidth(kLineWidth);
        mLeft.SetWidth(kLineWidth);
        mRight.SetWidth(kLineWidth);
    }

	public void UpdateRectangle(Vector3 center, Vector3 rightDir, float width, Vector3 upDir, float height)
    {
        rightDir.Normalize();
        rightDir = 0.5f * width * rightDir;
        upDir.Normalize();
        upDir = 0.5f * height * upDir;
        mCenter = center;
        mLL = center - rightDir - upDir;
        mLR = center + rightDir - upDir;
        mUL = center - rightDir + upDir;
        mUR = center + rightDir + upDir;
        mTop.SetEndPoints(mUL, mUR);
        mBottom.SetEndPoints(mLL, mLR);
        mLeft.SetEndPoints(mLL, mUL);
        mRight.SetEndPoints(mLR, mUR);
    }

	public Vector3 GetCenter() { return mCenter; }
    public Vector3 GetLL() { return mLL; }
    public Vector3 GetLR() { return mLR; }
    public Vector3 GetUL() { return mUL; }
    public Vector3 GetUR() { return mUR; }
    public float GetWidth() { return mTop.GetLineSegmentLength(); }
    public float GetHeight() { return mLeft.GetLineSegmentLength(); }

    public void Show(bool show)
    {
        mTop.gameObject.SetActive(show);
        mBottom.gameObject.SetActive(show);
        mLeft.gameObject.SetActive(show);
        mRight.gameObject.SetActive(show);
    }

}
