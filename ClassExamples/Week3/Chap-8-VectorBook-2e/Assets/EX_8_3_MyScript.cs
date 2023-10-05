using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_8_3_MyScript : MonoBehaviour
{
    public enum PcPositionMode {
        FromPc,
        FromP1,
        FromP2
    };

    public GameObject P1 = null;   // The first position
    public GameObject P2 = null;    // The second position
    public GameObject Pc = null;

    public bool DrawQuaternion = true;
    public PcPositionMode NextPcFrom = PcPositionMode.FromPc;
    
    private float kDeltaTheta = 30f;
    private const float kSmallAngle = 1f;

    #region For visualizing the vectors
    private float kDrawVectorLength = 2.0f;
    private float kDrawAxisLength = 3.0f;
    private MyQuaternion ShowQuat;  // To show the quaternion
    private MyVector ShowV1;
    private MyVector ShowVc;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P1 != null);   // Verify proper setting in the editor
        Debug.Assert(P2 != null);
        Debug.Assert(Pc != null);
        Pc.transform.localPosition = P1.transform.localPosition;
        
        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowQuat = new MyQuaternion
        {
            InitColor = Color.green,
            RotatedColor = Color.red
        };
        ShowQuat.ShowQuaternion = true;
        ShowV1 = new MyVector {
            VectorColor = Color.green
        };
        ShowVc = new MyVector {
            VectorColor = Color.blue
        };
        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(Pc, true);
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 V1n = (P1.transform.localPosition).normalized;  // V1 normalized
        Vector3 V2n = (P2.transform.localPosition).normalized;  // V2 normalized
        Vector3 Vcn = (Pc.transform.localPosition).normalized;  // Vector to current position

        float cosTheta = Vector3.Dot(V1n, V2n);
        if (Mathf.Abs(cosTheta) >= (1.0f-float.Epsilon)) {
            Debug.Log("V1 and V2 are almost parallel: cannot rotate to align");
            return; // V1 V2: almost parallel
        }

        float theta1 = Mathf.Acos(Vector3.Dot(Vcn, V1n)) * Mathf.Rad2Deg;
        float theta2 = Mathf.Acos(Vector3.Dot(Vcn, V2n)) * Mathf.Rad2Deg;

        float alpha = 0f;
        Vector3 axis = Vector3.zero;
        Vector3 Pf = Vector3.zero;
        if ((theta2 > kSmallAngle)) {
            switch (NextPcFrom) {
                case PcPositionMode.FromPc:
                    alpha = kDeltaTheta * Time.deltaTime;
                    axis = Vector3.Cross(Vcn, V2n);
                    Pf = Vcn;
                break;
                case PcPositionMode.FromP1:
                    alpha = theta1 + (kDeltaTheta * Time.deltaTime);
                    axis = Vector3.Cross(V1n, V2n);
                    Pf = V1n;
                break;
                case PcPositionMode.FromP2:
                    alpha = theta2 - (kDeltaTheta * Time.deltaTime);
                    axis = Vector3.Cross(V2n, V1n);
                    Pf = V2n;
                break;
            }           
            Vector4 q = QFromAngleAxis(alpha, axis);
            Pc.transform.localPosition = QRotation(q, Pf);
        } else {
            Pc.transform.localPosition = P1.transform.localPosition;
        }

        #region  For visualizing the vectors
        // Make sure axis passes through the origin
        axis = Vector3.Cross(P1.transform.localPosition, P2.transform.localPosition);
        if (axis.magnitude < float.Epsilon)
            return; // Two are aligned, or, one is zero vector
        ShowQuat.ShowRotation(kDrawAxisLength * axis.normalized, P1.transform.localPosition, P2.transform.localPosition);
        ShowQuat.ShowQuaternion = DrawQuaternion;
        ShowV1.VectorFromTo(Vector3.zero, kDrawVectorLength * Vector3.Normalize(P1.transform.localPosition));
        ShowVc.VectorFromTo(Vector3.zero, kDrawVectorLength * Vector3.Normalize(Pc.transform.localPosition));
        switch (NextPcFrom) {
            case PcPositionMode.FromPc:
                Pc.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case PcPositionMode.FromP1:
                Pc.GetComponent<Renderer>().material.color = Color.green;
                break;
            case PcPositionMode.FromP2:
                Pc.GetComponent<Renderer>().material.color = Color.red;
                break;
        }
        #endregion
    }

    // axis: should be a normalized vector
    // angle: rotation (in degrees)
    Vector4 QFromAngleAxis(float angle, Vector3 axis) {
        float useTheta = angle * Mathf.Deg2Rad * 0.5f;
        float sinTheta = Mathf.Sin(useTheta);
        float cosTheta = Mathf.Cos(useTheta);
        axis.Normalize();
        return new Vector4(sinTheta * axis.x, sinTheta * axis.y, sinTheta * axis.z, cosTheta);
    }

    // Computes quaternion product of q1 q2
    Vector4 QMultiplication(Vector4 q1, Vector4 q2) {
        Vector4 r;
        r.x = q1.w*q2.x + q1.x*q2.w + q1.y*q2.z - q1.z*q2.y;
        r.y = q1.w*q2.y - q1.x*q2.z + q1.y*q2.w + q1.z*q2.x;
        r.z = q1.w*q2.z + q1.x*q2.y - q1.y*q2.x + q1.z*q2.w; 
        r.w = q1.w*q2.w - q1.x*q2.x - q1.y*q2.y - q1.z*q2.z;
        return r;
    }

    // Rotate p based on the quaternion q
    Vector3 QRotation(Vector4 qr, Vector3 p) {
        Vector4 pq = new Vector4(p.x, p.y, p.z, 0);
        Vector4 qr_inv = new Vector4(-qr.x, -qr.y, -qr.z, qr.w); 
                // q-inv: is rotate by the same axis by -theta OR
                //        =rotate by the -axis by theta
                // in either case: it is the above;
        
        pq = QMultiplication(qr, pq);
        pq = QMultiplication(pq, qr_inv); 
        return new Vector3(pq.x, pq.y, pq.z);
    }

}