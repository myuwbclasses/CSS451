using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSystem : MonoBehaviour
{
    public SimpleMotionPhysics MySphere = null;
    public float LaunchSpeedScale = 20f;        // I played around, this number seem ok
    public float GravitationPull = -50f;        // I played around, this number seem ok
    public bool Launch = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(MySphere != null);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Launch)
        {
            Launch = false;

            float size = 2f * transform.localScale.y;  // use this as the "strenth" of the launcher

            MySphere.transform.localPosition = transform.localPosition + size * transform.up;  // put the sphere here
            MySphere.Velocity = size * LaunchSpeedScale * transform.up; // Follow the current up direction
            MySphere.Acceleration = Vector3.zero;  // Initial acceleration follow the current up
            MySphere.GravitationPull = GravitationPull * Vector3.up;
        }
        
    }
}
