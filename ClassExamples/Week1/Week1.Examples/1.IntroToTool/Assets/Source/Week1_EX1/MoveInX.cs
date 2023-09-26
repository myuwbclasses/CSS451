using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        // p.x += 1.0f/60.0f;
        p.x += 1.0f * Time.smoothDeltaTime;
        transform.localPosition = p;
    }
}
