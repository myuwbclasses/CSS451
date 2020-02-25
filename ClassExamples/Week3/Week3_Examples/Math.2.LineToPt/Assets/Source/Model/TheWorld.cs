using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject P1, P2;
    public GameObject Projected;
    public GameObject PA;

    private float kScaleFactor = 0.1f;
    private float kMaxSize = 5f;

    private void Start()
    {
        Debug.Assert(P1 != null);
        Debug.Assert(P2 != null);
        Debug.Assert(PA != null);

        Projected.GetComponent<Renderer>().material.color = Color.black;
    }

    private void Update()
    {
        float distant = 0;
        Vector3 V = P2.transform.localPosition - P1.transform.localPosition;
        float vMagnitude = V.magnitude;
        V /= vMagnitude; // <<-- what am I doing here?

        Vector3 VA = PA.transform.localPosition - P1.transform.localPosition;
        float d = Vector3.Dot(VA, V);

        if (d < 0)
        {
            distant = VA.magnitude;
        }
        else if (d > vMagnitude)
        {
            distant = (PA.transform.localPosition - P2.transform.localPosition).magnitude;
        } else
        {
            distant = Mathf.Sqrt(VA.sqrMagnitude - d * d);
        }

        float s = kMaxSize - (distant * kScaleFactor);
        Projected.transform.localScale = new Vector3(s, s, s);
        Projected.transform.localPosition = P1.transform.localPosition + d * V;

        // Draw Debug lines
        Debug.DrawLine(P1.transform.localPosition, P2.transform.localPosition, Color.black);
        Debug.DrawLine(PA.transform.localPosition, Projected.transform.localPosition, Color.red);
    }
}
