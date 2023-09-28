using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior: MonoBehaviour {
    static Vector3 sMin = new Vector3(-10f, -10f, -10f);
    static Vector3 sMax = new Vector3(10f, 10f, 10f);

    float mSpeed = 0.0f;
    Vector3 mVelocityDir = Vector3.right;

    // Use this for initialization
    void Start()
    {
        SetAsSelected();
        transform.gameObject.layer = 8; // in a different layer
    }
	
	// Update is called once per frame
	void Update () {
        if (mSpeed == 0f) // no motion 
            return;

        transform.localPosition = transform.localPosition + mSpeed * mVelocityDir;
        if ((transform.localPosition.x < sMin.x) || (transform.localPosition.x > sMax.x))
            mVelocityDir.x *= -1;
        if ((transform.localPosition.y < sMin.y) || (transform.localPosition.y > sMax.y))
            mVelocityDir.y *= -1;
        if ((transform.localPosition.z < sMin.z) || (transform.localPosition.z > sMax.z))
            mVelocityDir.z *= -1;
    }

    public void SetSize(ref Vector3 pos)
    {
        Vector3 delta = transform.position - pos;
        delta.y = 0f; // let's not allow vertical component
        float size = delta.magnitude;
        if (size < 0.001f)
            return;

        transform.localScale = new Vector3(2 * size, 2 * size, 2 * size);
        mVelocityDir = delta / size;
    }

    public void EnableMotion()
    {
        float size = transform.localScale.x; // assume x,y,z are the same
        mSpeed = size / 30f;  // movement speed is porportional to sphere size
    }

    public void SetAsSelected()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void ReleaseSelection()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void SetPosition(float x, float z)
    {
        transform.localPosition = new Vector3(x, 0, z);
    }
}
