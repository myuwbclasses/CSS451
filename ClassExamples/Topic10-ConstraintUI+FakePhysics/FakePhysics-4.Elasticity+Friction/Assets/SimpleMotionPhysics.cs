using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMotionPhysics : MonoBehaviour
{
    public Vector3 GravitationPull = -Vector3.up;  // default is dropping downwards
    public Vector3 Acceleration = Vector3.zero;  // default is dropping downwards
    public Vector3 Velocity = Vector3.zero;

    // These should be set according to the physical properties of the colliding object
    public float Friction = 0f;     // Slow down in the direction that is perpendicular to the collision normal
    public float Elasticity = 1.0f; // Slow down in the direction of the normal

    private int TimeAlive = 0;
    const int kTimeToDestroy = 600;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimeAlive++;
        if (TimeAlive > kTimeToDestroy)
            Destroy(gameObject);

        Vector3 n = Vector3.zero;  // normal at collision 
        if (CollideWithObject(out n))
        {
            n.Normalize();
            Velocity = Vector3.Reflect(Velocity, n);

            // we want to decompose the Velocity into two components
            //     one in the direction of the normal of collision <-- elasticity will affect this
            //     one in the direction that is perpenticular to the normal <-- friction will affect this

            Vector3 v = Velocity.normalized;
            // First check if Velocity and N are parallel
            if (Mathf.Abs(Vector3.Dot(n, v)) > 0.99f)  // almost parallel
            {
                Velocity *= Elasticity; // simply slow down
            } else
            {
#if KelvinOriginal
                // if not parallel, then, we can construct an axisframe
                Vector3 w = Vector3.Cross(n, v); // in the direction perpendicular to both n and v
                Vector3 u = Vector3.Cross(w, n); // in the direction perpendicular to both n and w
                //
                // now, n, w, u are three perpendicular vectors
                // we know w and u are perpendicular to n
                w.Normalize();
                u.Normalize();

                float nSize = Vector3.Dot(n, Velocity);
                float wSize = Vector3.Dot(w, Velocity);
                float uSize = Vector3.Dot(u, Velocity);
                Vector3 nDir = nSize * n;
                Vector3 tDir = wSize * w + uSize * u;
                Velocity = Elasticity * nDir + (1f-Friction) * tDir;
#else
                // Di Wang in class pointed out the above is unnecessary

                // Decompose into n-direction and the rest
                float nSize = Vector3.Dot(n, Velocity);
                Vector3 nDir = nSize * n;
                Vector3 tDir = Velocity - nDir; // t as in direction that is tangential to the normal
                // 
                Velocity = Elasticity * nDir + (1f - Friction) * tDir;
#endif
            }
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
