using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMotionPhysics : MonoBehaviour
{
    public Vector3 GravitationPull = -Vector3.up;  // default is dropping downwards

    public Vector3 Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y <= 0f)
        {
            Velocity = Vector3.zero;
            return;  // for now
        }

        Velocity += GravitationPull * Time.smoothDeltaTime;               // change in Velocity @GravitationPull
        transform.localPosition += Velocity * Time.smoothDeltaTime;    // change in position
    }
}
