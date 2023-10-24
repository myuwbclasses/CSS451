using UnityEngine;

public class EX_8_5_MyScript : MonoBehaviour
{
    public GameObject Po = null;    // Origin of the reference frame
    public GameObject Px = null;    // X-position defining the x-axis
    public GameObject Pz = null;    // Z-position defining the z-axis
    public GameObject AlignX = null;   // Use own operator, align first axis
    public GameObject AlignXY = null;   // Use own operator, align both axes
    public GameObject AlignUnity = null;   // Align this frame using unity's rotation
    
    private const float kSmallAngle = 1f;
    #region For visualizing the vectors
    private const float kAxisLength = 3.0f;    
    private MyAxisFrame ShowRF, uShowAF, ShowAF1, ShowAF2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Po != null);   // Verify proper setting in the editor
        Debug.Assert(Px != null);
        Debug.Assert(Pz != null);
        Debug.Assert(AlignX != null);
        Debug.Assert(AlignXY != null);
        Debug.Assert(AlignUnity != null);
        
        #region For visualizing the vectors
        ShowRF = new MyAxisFrame();
        ShowAF1 = new MyAxisFrame();
        ShowAF2 = new MyAxisFrame();
        uShowAF = new MyAxisFrame();

        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(Po, true);
        sv.DisablePicking(AlignX, true);
        sv.DisablePicking(AlignXY, true);
        sv.DisablePicking(AlignUnity, true);
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        // Assume without checking:
        //    Po, Py, and Pz
        // Are proper positions where: PoPy is along the y-axis, PoPz is along the Z-axis
        Vector3 vxr = (Px.transform.position - Po.transform.position).normalized;
        Vector3 vzr = (Pz.transform.position - Po.transform.position).normalized;
        Vector3 vyr = Vector3.Cross(vzr, vxr);

        Quaternion qUnity = Quaternion.LookRotation(vzr, vyr);
        AlignUnity.transform.localRotation = qUnity;

        Vector4 qx = QAlignVectors(Vector3.right, vxr); 
        AlignX.transform.localRotation = V4ToQ(qx);
                        
        Vector4 qy = QAlignVectors(AlignX.transform.up, vyr);
        Vector4 qc = QMultiplication(qy, qx);
        AlignXY.transform.localRotation = V4ToQ(qc);
        
        #region  For visualizing the vectors
        //ax = Vector3.Cross(ay, az);

        // Make sure axis passes through the origin
        ShowRF.At = Po.transform.position;
        ShowRF.SetFrame(vxr, vyr, vzr);

        ShowAF1.At = AlignX.transform.localPosition -
                    0.5f * AlignX.transform.localScale.x * AlignX.transform.right -
                    0.5f * AlignX.transform.localScale.y * AlignX.transform.up -
                    0.5f * AlignX.transform.localScale.z * AlignX.transform.forward;
        ShowAF1.SetFrame(AlignX.transform.right, AlignX.transform.up, AlignX.transform.forward);


        ShowAF2.At = AlignXY.transform.localPosition -
                    0.5f * AlignXY.transform.localScale.x * AlignXY.transform.right -
                    0.5f * AlignXY.transform.localScale.y * AlignXY.transform.up -
                    0.5f * AlignXY.transform.localScale.z * AlignXY.transform.forward;
        ShowAF2.SetFrame(AlignXY.transform.right, AlignXY.transform.up, AlignXY.transform.forward);

        uShowAF.At = AlignUnity.transform.localPosition -
                    0.5f * AlignUnity.transform.localScale.x * AlignUnity.transform.right -
                    0.5f * AlignUnity.transform.localScale.y * AlignUnity.transform.up -
                    0.5f * AlignUnity.transform.localScale.z * AlignUnity.transform.forward;
        uShowAF.SetFrame(AlignUnity.transform.right, AlignUnity.transform.up, AlignUnity.transform.forward);
        #endregion
    }

    Quaternion V4ToQ(Vector4 q) {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }

     Vector4 QAlignVectors(Vector3 from, Vector3 to) {
        from.Normalize();
        to.Normalize();
        float theta = Mathf.Acos(Vector3.Dot(from, to)) * Mathf.Rad2Deg;
        Vector4 q = new Vector4(0, 0, 0, 1); // Quaternion identity
        if (theta > kSmallAngle) {
            Vector3 axis = Vector3.Cross(from, to);
            q = QFromAngleAxis(theta, axis);
        }
        return q;
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