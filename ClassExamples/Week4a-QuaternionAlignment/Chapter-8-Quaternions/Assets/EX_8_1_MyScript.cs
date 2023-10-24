using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_8_1_MyScript : MonoBehaviour
{
    public GameObject A = null;   // The axis of rotation
    public GameObject Pi = null;    // initial position
    public GameObject Pr = null;    // rotated position
    public float Theta = 30.0f;

    public bool DrawQuaternion  = true;

    #region For visualizing the vectors
    private MyQuaternion ShowQuat;  // To show the quaternion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(A != null);   // Verify proper setting in the editor
        Debug.Assert(Pi != null);
        Debug.Assert(Pr != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowQuat = new MyQuaternion
        {
            InitColor = Color.green,
            RotatedColor = Color.red
        };
        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(Pr, true);
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = A.transform.localPosition;
        Vector4 q = QFromAngleAxis(Theta, axis);
        Pr.transform.localPosition = QRotation(q, Pi.transform.localPosition);

        #region  For visualizing the vectors
        // Make sure axis passes through the origin
        ShowQuat.ShowRotation(A.transform.localPosition, Pi.transform.localPosition, Pr.transform.localPosition);
        ShowQuat.ShowQuaternion = DrawQuaternion;
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
        r.x =  q1.x*q2.w + q1.y*q2.z - q1.z*q2.y + q1.w*q2.x;
        r.y = -q1.x*q2.z + q1.y*q2.w + q1.z*q2.x + q1.w*q2.y;
        r.z =  q1.x*q2.y - q1.y*q2.x + q1.z*q2.w + q1.w*q2.z; 
        r.w = -q1.x*q2.x - q1.y*q2.y - q1.z*q2.z + q1.w*q2.w;
        return r;
    }

    // Rotate p based on the quaternion q
    Vector3 QRotation(Vector4 qr, Vector3 p) {
        Vector4 pq = new Vector4(p.x, p.y, p.z, 0);
        Vector4 qr_inv = new Vector4(-qr.x, -qr.y, -qr.z, qr.w); 
                // q-inv: is rotate by the same axis by -theta OR
                //        =rotate by the -axis by theta
                // in either case: it is the above;
                // Vector4(q.x, q.y, q.z, -q.w); <-- this is NOT the q_inv
    
        pq = QMultiplication(qr, pq);
        pq = QMultiplication(pq, qr_inv); 
        // Debug.Log("pq: w=" + pq.w);
        return new Vector3(pq.x, pq.y, pq.z);
    }
}