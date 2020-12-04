using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMotionPhysics : MonoBehaviour
{
    public Vector3 GravitationPull = -Vector3.up;  // default is dropping downwards
    public Vector3 Acceleration = Vector3.zero;  // default is dropping downwards
    public Vector3 Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 n = Vector3.zero;  // normal at collision 
        if (CollideWithObject(out n))
        {
            Velocity = Vector3.Reflect(Velocity, n);
        }

        Acceleration += GravitationPull * Time.smoothDeltaTime;        // change in acceleration
        Velocity += Acceleration * Time.smoothDeltaTime;               // change in Velocity
        transform.localPosition += Velocity * Time.smoothDeltaTime;    // change in position
    }


    // Assumes the simplest case of a plane at XZ plane with Y=0
    private bool CollideWithObject(out Vector3 normal)
    {
        bool status = false;
        normal = Vector3.up;
        status = (transform.localPosition.y + transform.localScale.x) < 0f;  // assume uniformly scaled
        // To hack no inter-penetration
        if (status)
        {
            Vector3 p = transform.localPosition;
            p.y = 0.01f; // make sure this is slightly above the ground;
            transform.localPosition = p;
        }
                
        return status;
    }
}
