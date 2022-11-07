using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithPlane : MonoBehaviour
{
    public Transform ThePlane = null;  // Notice this is the Transform, 
                                       //    we don't need the GameObject, just xform will do

    public float MyHeight = 1f;   // how much to move
    public bool AlignYOnly = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(ThePlane != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (AlignYOnly) {
            transform.localRotation = Quaternion.FromToRotation(Vector3.up, ThePlane.up);
        }
        else
        {
            transform.localRotation = ThePlane.localRotation;   // align the two objects
        }



        transform.localPosition = ThePlane.localPosition + MyHeight * transform.up;
        

    }
}
