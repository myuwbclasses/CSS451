using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject P1, P2;

    private void Start()
    {
        Debug.Assert(P1 != null);
        Debug.Assert(P2 != null);
    }

    private void Update()
    {
        Debug.DrawLine(P1.transform.localPosition, P2.transform.localPosition, Color.black);
    }
}
