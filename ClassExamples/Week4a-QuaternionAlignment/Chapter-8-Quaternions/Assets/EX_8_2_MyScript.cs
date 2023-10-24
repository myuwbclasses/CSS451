using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_8_2_MyScript : MonoBehaviour
{
    public GameObject Pi = null;    // initial position

    public GameObject Pr = null;    // rotated position
    public GameObject A = null;   // The axis of rotation
    public float Theta = 30.0f;
    public bool DrawQuaternion = true;

    public GameObject Pr1 = null;    // rotated position
    public GameObject A1 = null;   // Axis for second rotation
    public float Theta1 = 40f;
    public bool DrawQuaternion1 = true;

    public GameObject Pr2 = null;    // rotated position
    public GameObject A2 = null;   // Axis for third rotation
    public float Theta2 = 50f;
    public bool DrawQuaternion2 = true;

    public GameObject Pc = null;  // Rotation result from concatenated operator
    public bool DrawPc = false;

    #region For visualizing the vectors
    MyQuaternion ShowQuat, ShowQuat1, ShowQuat2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Pi != null); // Verify proper setting in the editor
        Debug.Assert(Pr != null);
        Debug.Assert(A != null);   
        Debug.Assert(Pr1 != null);
        Debug.Assert(A1 != null);   // Verify proper setting in the editor
        Debug.Assert(Pr2 != null);
        Debug.Assert(A2 != null);
        Debug.Assert(Pc != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowQuat = new MyQuaternion
        {
            InitColor = Color.green,
            RotatedColor = Color.red
        };
        ShowQuat1 = new MyQuaternion
        {
            InitColor = Color.red,
            RotatedColor = Color.blue
        };
        ShowQuat2 = new MyQuaternion
        {
            InitColor = Color.blue,
            RotatedColor = Color.black
        };
        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(Pr, true);
        sv.DisablePicking(Pr1, true);
        sv.DisablePicking(Pr2, true);
        sv.DisablePicking(Pc, true);
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 q = QFromAngleAxis(Theta, A.transform.localPosition);  
        Vector4 q1 = QFromAngleAxis(Theta1, A1.transform.localPosition);
        Vector4 q2 = QFromAngleAxis(Theta2, A2.transform.localPosition);

        Pr.transform.localPosition = QRotation(q, Pi.transform.localPosition);
        Pr1.transform.localPosition = QRotation(q1, Pr.transform.localPosition);       
        Pr2.transform.localPosition = QRotation(q2, Pr1.transform.localPosition);

        Vector4 qc = QMultiplication(q1, q);
        qc = QMultiplication(q2, qc);
        Pc.transform.localPosition = QRotation(qc, Pi.transform.localPosition);
        
        #region  For visualizing the vectors
        // To avoid confusion
        A.SetActive(DrawQuaternion);
        A1.SetActive(DrawQuaternion1);
        A2.SetActive(DrawQuaternion2);
        Pc.SetActive(DrawPc);

        ShowQuat.ShowRotation(A.transform.localPosition, Pi.transform.localPosition, Pr.transform.localPosition);
        ShowQuat.ShowQuaternion = DrawQuaternion;
        ShowQuat1.ShowRotation(A1.transform.localPosition, Pr.transform.localPosition, Pr1.transform.localPosition);
        ShowQuat1.ShowQuaternion = DrawQuaternion1;
        ShowQuat2.ShowRotation(A2.transform.localPosition, Pr1.transform.localPosition, Pr2.transform.localPosition);
        ShowQuat2.ShowQuaternion = DrawQuaternion2;
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