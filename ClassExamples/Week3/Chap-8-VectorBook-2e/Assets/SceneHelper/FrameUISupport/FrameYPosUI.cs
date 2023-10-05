using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameYPosUI : MonoBehaviour
{
    private const float kAxisPointDistance = 2.0f;
    public Transform origin, zPos;
    private Vector3 oldPos;
    private Vector3 yDir, zDir;
    public FrameYPosUI OtherPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.localPosition;
        yDir = origin.localPosition - oldPos;
        zDir = zPos.localPosition - oldPos;
    }

    // Update is called once per frame
    void Update()
    {
        if ((oldPos - transform.localPosition).magnitude > 0.1f) {
            // derive the rotation
            Vector3 nv = (transform.localPosition - origin.localPosition).normalized;
            Vector3 ov = (oldPos - origin.localPosition).normalized;
            Quaternion q = Quaternion.FromToRotation(ov, nv);

            Vector3 zP = (OtherPos.transform.localPosition - origin.localPosition).normalized;
            zP = q * zP;
            zPos.localPosition = origin.localPosition + (zP * kAxisPointDistance);
            OtherPos.oldPos = zPos.localPosition;

            transform.localPosition = origin.localPosition + nv * kAxisPointDistance;
            oldPos = transform.localPosition;
        }
    }
}
