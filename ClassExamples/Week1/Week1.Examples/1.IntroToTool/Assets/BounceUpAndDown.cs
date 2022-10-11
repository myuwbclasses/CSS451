using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUpAndDown: MonoBehaviour {

    public float yRange = 10f;
    private float fDelta = 0.1f;

    public bool shouldBounce = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!shouldBounce) 
            return;
        Vector3 p = transform.position;
        p.y += fDelta;
        if (Mathf.Abs(p.y) > yRange)
        {
            fDelta *= -1f;

            // Show how to find an object
            GameObject obj = GameObject.Find("MyCylinder");
            if (obj != null)
            {
                Vector3 cp = obj.transform.position;
                cp.y += (10f * fDelta);
                obj.transform.position = cp;
            }

            // show how to access components which may or may not be there
            Renderer r = GetComponent<Renderer>();
            if (r != null)
            {
                Material m = r.material;
                Color c = m.color;
                c.r = 1f - c.r;
                m.color = c;
            }
        }
        transform.position = p;
	}

    public void toggleBounce() {
        shouldBounce = !shouldBounce;
    }
}
