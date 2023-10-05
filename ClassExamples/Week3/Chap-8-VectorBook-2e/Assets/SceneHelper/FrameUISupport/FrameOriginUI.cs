using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameOriginUI : MonoBehaviour
{
    public Transform yPos, zPos;
    private Vector3 oldPos;
    private Vector3 yDir, zDir;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.localPosition;
        yDir = yPos.localPosition - oldPos;
        zDir = zPos.localPosition - oldPos;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ((oldPos - transform.localPosition).magnitude > 0.1f) {
            oldPos = transform.localPosition;
            yPos.localPosition = oldPos + yDir;
            zPos.localPosition = oldPos + zDir;
        }
        */
        
    }
}
