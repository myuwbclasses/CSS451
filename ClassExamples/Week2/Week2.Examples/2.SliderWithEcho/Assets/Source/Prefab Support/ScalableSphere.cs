using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableSphere: MonoBehaviour {

    const float kSize = 3f;
    public void SetSpherePosition(Vector3 p)
    {
        transform.localPosition = p;
        SetSize(kSize);
    }

    public void SetSize(float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
