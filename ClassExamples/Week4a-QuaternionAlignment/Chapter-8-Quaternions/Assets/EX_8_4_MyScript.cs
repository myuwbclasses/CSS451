using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_8_4_MyScript : MonoBehaviour
{
    public GameObject Po = null;    // Observer position
    public GameObject Pt = null;    // Target position

    public GameObject Pa = null;    // Agent position
    public bool ActivateAgent = false;
    public  float Rate = 0.8f;

    private Vector3 Vot = Vector3.right; // (1,0, 0)
    private Vector3 Vat =  Vector3.right; // (1, 0, 0)    

    private const float kAgentSpeed = 0.01f;
    private const float kSmallAngle = 1f;

    #region For visualizing the vectors    
    private MyVector Po2Pt;
    private MyVector PaDir;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Po != null);    // Verify proper setting in the editor
        Debug.Assert(Pt != null);
        Debug.Assert(Pa != null);
        
        
        #region For visualizing the vectors
        // To support visualizing the vectors
        Po2Pt = new MyVector {
            VectorColor = Color.green,
            Magnitude = 2.0f
        };
        PaDir = new MyVector {
            VectorColor = Color.green,
            Magnitude = 2.0f
        };
        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(Pa, true);
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 o2t = Pt.transform.localPosition - Po.transform.localPosition;
        Vot = AlignVectors(Vot, o2t, Rate);
        
        if (ActivateAgent) {
            Vector3 a2t = Pt.transform.localPosition - Pa.transform.localPosition;
            Vat = AlignVectors(Vat, a2t, Rate);
            Pa.transform.localPosition += kAgentSpeed * Vat;
        } else {
            Pa.transform.localPosition = Po.transform.localPosition;
            Vat = Vector3.right;
        }

        #region  For visualizing the vectors
        Pa.SetActive(ActivateAgent);
        // Make sure axis passes through the origin
        Po2Pt.VectorAt = Po.transform.localPosition;
        Po2Pt.Direction = Vot;

        PaDir.DrawVector = ActivateAgent;
        PaDir.VectorAt = Pa.transform.localPosition;
        PaDir.Direction = Vat;
        #endregion
    }

    Vector3 AlignVectors(Vector3 from, Vector3 to, float rate) {
        from.Normalize();
        to.Normalize();
        float theta = Mathf.Acos(Vector3.Dot(from, to)) * Mathf.Rad2Deg;
        Vector4 q = new Vector4(0, 0, 0, 1); // Quaternion identity
        if (theta > kSmallAngle) {
            Vector3 axis = Vector3.Cross(from, to);
            q = QFromAngleAxis(rate * Time.smoothDeltaTime * theta, axis);
        }
        return QRotation(q, from);
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