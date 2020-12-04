using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSystem : MonoBehaviour
{
    public float LaunchSpeedScale = 20f;        // I played around, this number seem ok
    public float GravitationPull = -50f;        // I played around, this number seem ok
    public bool Launch = false;

    // These should NOT be defined here, but for convenience of testing ...
    public float Friction = 0f;
    public float Elasticity = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Launch)
        {
            float size = 2f * transform.localScale.y;  // use this as the "strenth" of the launcher

            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            SimpleMotionPhysics s = g.AddComponent<SimpleMotionPhysics>();
            s.transform.localPosition = transform.localPosition + size * transform.up;  // put the sphere here
            s.Velocity = size * LaunchSpeedScale * transform.up; // Follow the current up direction
            s.Acceleration = Vector3.zero;  // Initial acceleration follow the current up
            s.GravitationPull = GravitationPull * Vector3.up;
            s.Friction = Friction;
            s.Elasticity = Elasticity;
        }
        
    }
}
