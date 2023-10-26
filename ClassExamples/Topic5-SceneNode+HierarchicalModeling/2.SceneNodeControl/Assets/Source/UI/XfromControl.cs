using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class XfromControl : MonoBehaviour {
    public Toggle T, R, S;
    public SliderWithEcho X, Y, Z;
    public TextMeshProUGUI ObjectName;

    private Transform mSelected;
    private Vector3 mPreviousSliderValues = Vector3.zero;

	// Use this for initialization
	void Start () {
        T.onValueChanged.AddListener(SetToTranslation);
        R.onValueChanged.AddListener(SetToRotation);
        S.onValueChanged.AddListener(SetToScaling);
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);

        T.isOn = false;
        R.isOn = true;
        S.isOn = false;
        SetToRotation(true);
	}
	
    //---------------------------------------------------------------------------------
    // Initialize slider bars to specific function
    void SetToTranslation(bool v)
    {
        Vector3 p = ReadObjectXfrom();
        mPreviousSliderValues = p;
        X.InitSliderRange(-20, 20, p.x);
        Y.InitSliderRange(-20, 20, p.y);
        Z.InitSliderRange(-20, 20, p.z);
    }

    void SetToScaling(bool v)
    {
        Vector3 s = ReadObjectXfrom();
        mPreviousSliderValues = s;
        X.InitSliderRange(0.1f, 5, s.x);
        Y.InitSliderRange(0.1f, 5, s.y);
        Z.InitSliderRange(0.1f, 5, s.z);
    }

    void SetToRotation(bool v)
    {
        Vector3 r = ReadObjectXfrom();
        mPreviousSliderValues = r;
        X.InitSliderRange(-180, 180, r.x);
        Y.InitSliderRange(-180, 180, r.y);
        Z.InitSliderRange(-180, 180, r.z);
        mPreviousSliderValues = r;
    }
    //---------------------------------------------------------------------------------

    //---------------------------------------------------------------------------------
    // resopond to sldier bar value changes
    void XValueChanged(float v)
    {
        if (mSelected == null)
            return;
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasted
            float dx = v - mPreviousSliderValues.x;
            mPreviousSliderValues.x = v;
        Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);  // **- Please read the notes at the end
        p.x = v;
        UISetObjectXform(ref p, ref q);
    }
    
    void YValueChanged(float v)
    {
        if (mSelected == null)
            return;
        Vector3 p = ReadObjectXfrom();
            // if not in rotation, next two lines of work would be wasted
            float dy = v - mPreviousSliderValues.y;
            mPreviousSliderValues.y = v;
        Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);    // **- Please read the notes at the end
        p.y = v;        
        UISetObjectXform(ref p, ref q);
    }

    void ZValueChanged(float v)
    {
        if (mSelected == null)
            return;
        Vector3 p = ReadObjectXfrom();
            // if not in rotation, next two lines of work would be wasterd
            float dz = v - mPreviousSliderValues.z;
            mPreviousSliderValues.z = v;
        Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward); // **- Please read the notes at the end
        p.z = v;
        UISetObjectXform(ref p, ref q);
    }
    //---------------------------------------------------------------------------------

    // new object selected
    public void SetSelectedObject(Transform xform)
    {
        mSelected = xform;
        mPreviousSliderValues = Vector3.zero;
        if (xform != null)
            ObjectName.text = "Selected:" + xform.name;
        else
            ObjectName.text = "Selected: none";
        ObjectSetUI();
    }

    public void ObjectSetUI()
    {
        Vector3 p = ReadObjectXfrom();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }

    private Vector3 ReadObjectXfrom()
    {
        Vector3 p;
        
        if (T.isOn)
        {
            if (mSelected != null)
                p = mSelected.localPosition;
            else
                p = Vector3.zero;
        }
        else if (S.isOn)
        {
            if (mSelected != null)
                p = mSelected.localScale;
            else
                p = Vector3.one;
        }
        else
        {
            p = Vector3.zero;
        }
        return p;
    }

    private void UISetObjectXform(ref Vector3 p, ref Quaternion q)
    {
        if (mSelected == null)
            return;

        if (T.isOn)
        {
            mSelected.localPosition = p;
        }
        else if (S.isOn)
        {
            mSelected.localScale = p;
        } else
        {
            mSelected.localRotation = mSelected.localRotation * q; // **- Please read the notes at the end
        }
    }

    /* ** - Note on Quaternion rotation
    
    The order of concatenating quaternions is important.

        qc = q2 * q1

    Says, q1 rotation occurs BEFORE q2 (q1 first and then q2)

    In this case, the concatnation of subsequent rotations along the major axes

        localRotaiton = localRotation * qr 

            where qr = NewRotation_AlongMajorAxis (either x, y, or z)

    says, qr roation is applied _before_ the current rotation. This is obviously _NOT_ what happens, 
    the user's latest rotation should be applied _last_. So, what we want is:

         localRoation = qr' * localRotation

    where qr' is rotation along the _rotated_ major axes (rotated x, y, or z). Interestingly, 

        localRotation = qr' * locationRotation = locationRotate * qr

    Note: the difference between qr' and qr is the axis of rotation (before and after the localRotation).

    This can be verified by, e.g., for x-axis rotation by theta,

        qr = QFromAxis([1, 0, 0], theta)
        qr' = QFromAxis(Axis, theta)
    where, Axis, is
        Column-0 of RotationMatrix-of-localRotation
    */
}