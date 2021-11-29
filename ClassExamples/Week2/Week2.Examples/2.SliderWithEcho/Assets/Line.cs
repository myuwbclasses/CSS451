using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform p1, p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = p2.localPosition - p1.localPosition;
        transform.up = v;
        transform.localPosition = p1.localPosition + v * 0.5f;
        transform.localScale = new Vector3(0.1f, v.magnitude*0.5f, 0.1f);
    }
}
