using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject ThePlane;
    public GameObject ThePoint;
    public GameObject PlaneNormal;
    public GameObject Projected;
    

    private void Start()
    {
        ThePoint.GetComponent<Renderer>().material.color = Color.black;
        Projected.GetComponent<Renderer>().material.color = Color.red;
    }

    float kNormalSize = 5f;
    float kMaxProjectedSize = 10f;
    private void Update()
    {
        // the plane and its normal
        Vector3 n = -ThePlane.transform.forward;
        Vector3 center = ThePlane.transform.localPosition;
        Vector3 pt = center + kNormalSize * n;
        float d = Vector3.Dot(n, center);

        float h = Vector3.Dot(ThePoint.transform.localPosition, n) - d;
        Projected.transform.localPosition = ThePoint.transform.localPosition - (n * h);
        float s = h * 0.50f;
        if (s < 0)
            s = 0.5f;
        Projected.transform.localScale = new Vector3(s, s, s);
        Debug.DrawLine(Projected.transform.localPosition, ThePoint.transform.localPosition, Color.black);

        // normal
        float size = h / 2;
        Vector3 scale = PlaneNormal.transform.localScale;
        scale.y = size;
        PlaneNormal.transform.localScale = scale;
        PlaneNormal.transform.localRotation = Quaternion.FromToRotation(Vector3.up, n);
        PlaneNormal.transform.localPosition = Projected.transform.localPosition + (size * PlaneNormal.transform.up);


        Debug.DrawLine(center, pt, Color.black);
        // for debugging: Debug.Log(hit + ":" + details);
        
    }
}
