using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveller: MonoBehaviour
{
    float t = 120;
    float D = 0;
    Vector3 velocity = Vector3.zero;
    public GameObject P1, P2;

    // This is BAD for many reason ... but for simple demo we are ok
    public SliderWithEcho mTimeSlider;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;

        mTimeSlider.InitSliderRange(1, 600, 120);
        mTimeSlider.SetSliderLabel("Time");
        mTimeSlider.SetSliderListener(SetNewTime);
    }

    private void Update()
    {
        CheckForReset();
        ComputeVelocity();
        
        // regular update
        transform.localPosition += (D / t) * velocity;        
    }

    private void ComputeVelocity()
    {
        velocity = P2.transform.localPosition - P1.transform.localPosition;
        D = velocity.magnitude;
        velocity.Normalize();
    }

    private void CheckForReset()
    {
        // check to see if we should reset position
        Vector3 v = transform.localPosition - P1.transform.localPosition;

        if (v.magnitude > D)
            transform.localPosition = P1.transform.localPosition;
    }

    private void SetNewTime(float nt)
    {
        t = nt;
    }
}
