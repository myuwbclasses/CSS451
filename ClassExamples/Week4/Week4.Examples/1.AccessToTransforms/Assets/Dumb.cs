using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumb : MonoBehaviour
{
    public Transform  ShowNormal, ShowOn;
    public Vector3 Vn = Vector3.up;  // plane equation specified by the user
    public float D = 0f;

    // Update is called once per frame
    void Update()
    {
        // setting the plane to follow the equation
        Vn.Normalize();
        transform.up = Vn;
        transform.localPosition = D * Vn;

        // set Pon
        ShowOn.localPosition = D * Vn;

        // I want my ShowNormal (cylinder) to point in the Vn direction
        ShowNormal.up = Vn;
        ShowNormal.localPosition = D * Vn;
    }
}
