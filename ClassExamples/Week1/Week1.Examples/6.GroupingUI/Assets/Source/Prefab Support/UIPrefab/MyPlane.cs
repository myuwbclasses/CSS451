using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlane : MonoBehaviour
{
    public Transform TheLine = null;

    public Vector3 Vn = Vector3.up; // Plane normal
    public float D = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vn;
        Vn.Normalize();
        transform.localPosition = D*Vn;

        TheLine.up = Vn;
        TheLine.localPosition = transform.localPosition + 5f * Vn;
        TheLine.localScale = new Vector3(0.1f, 5f, 0.1f);
    }
}
