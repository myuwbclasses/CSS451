using UnityEngine;
using System.Collections;

public partial class CameraManipulation : MonoBehaviour
{
    public Transform LookAt;
    public bool DrawFrustum = false; 

    private float mMouseX = 0f;
    private float mMouseY = 0f;
    private const float kPixelToDegree = 0.1f;
    private const float kPixelToDistant = 0.05f;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(LookAt != null);
        InitializeFrustum();
        InitShowMouseClick();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrustumPosition();

        // this will change the rotation
        transform.LookAt(LookAt.transform);

        // Check to make sure mouse position is indeed in the viewport
        if (!MouseInCameraViewport())
            return;

        if (Input.GetKey(KeyCode.LeftAlt) &&
            (Input.GetMouseButtonDown(0) || (Input.GetMouseButtonDown(1))))
        {
            mMouseX = Input.mousePosition.x;
            mMouseY = Input.mousePosition.y;
            // Debug.Log("MouseButtonDown 0: (" + mMouseX + " " + mMouseY);
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && 
                (Input.GetMouseButton(0) || (Input.GetMouseButton(1))))
        {
            float dx = mMouseX - Input.mousePosition.x;
            float dy = mMouseY - Input.mousePosition.y;

            // annoying bug: 
            //     If MouseClick move AND THEN ALT-key
            //     Encounter jump because mMouseX and mMouseY not initialized

            mMouseX = Input.mousePosition.x;
            mMouseY = Input.mousePosition.y;

            if (Input.GetMouseButton(0)) // Camera Rotation
            { 
                RotateCameraAboutUp(-dx * kPixelToDegree);
                RotateCameraAboutSide(dy * kPixelToDegree);
            } else if (Input.GetMouseButton(1)) // Camera Panning
            {
                Vector3 delta = dx * kPixelToDistant * transform.right + dy * kPixelToDistant * transform.up;
                transform.localPosition += delta;
                LookAt.localPosition += delta;
            } 
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Vector2 d = Input.mouseScrollDelta;
            // move camera position towards LookAt
            Vector3 v = transform.localPosition - LookAt.localPosition;
            float dist = v.magnitude;
            v /= dist;
            float m = dist - d.y;
            transform.localPosition = LookAt.localPosition + m * v;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            ShowScreenToWorld(Input.mousePosition);
        }
    }

    private void RotateCameraAboutUp(float degree)
    {
        Quaternion up = Quaternion.AngleAxis(degree, transform.up);
        RotateCameraPosition(ref up);
    }

    private void RotateCameraAboutSide(float degree)
    {
        Quaternion side = Quaternion.AngleAxis(degree, transform.right);
        RotateCameraPosition(ref side);
    }

    private void RotateCameraPosition(ref Quaternion q)
    {
        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAt.localPosition, Quaternion.identity, Vector3.one);
        Matrix4x4 m = invP.inverse * r * invP;

        Vector3 newCameraPos = m.MultiplyPoint(transform.localPosition);
        if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) < 0.985)
        {
            transform.localPosition = newCameraPos;

            // First way:
                    // transform.LookAt(LookAt);
            // Second way:
                // Vector3 v = (LookAt.localPosition - transform.localPosition).normalized;
                // transform.localRotation = Quaternion.LookRotation(v, Vector3.up);
            // Third way: do everything ourselve!
                Vector3 v = (LookAt.localPosition - transform.localPosition).normalized;
                Vector3 w = Vector3.Cross(v, transform.up).normalized;
                Vector3 u = Vector3.Cross(w, v).normalized;
                // INTERESTING: 
                //    chaning the following directions must be done in specific sequence!
                //    E.g., NONE of the following order works: 
                //          Forward, Up, Right 
                //          Forward, Right, Up 
                //          Right, Forward, Up 
                //          Up, Forward, Right 
                //
                //   Forward-Vector MUST BE set LAST!!: both of the following works!
                //          Right, Up, Forward
                //          Up, Right, Forward
                transform.up = u;
                transform.right = w;
                transform.forward = v;
        }
    }

    public void SetLookAtPos(Vector3 p)
    {
        LookAt.localPosition = p;
    }

    // remember: 
    //      1. viewport for a camera is always normalized!
    //    Here we the MainCamera covers the entire window
    //    All other camera viewports DO NOT overlap
    bool MouseInCameraViewport()
    {
        Vector3 viewportPt;
        Camera c = GetComponent<Camera>(); // assume this exists!
        if (c == Camera.main) // check to see if mouse in some other camera's viewport
        {
            // Main camera has the lowest priority in terms of mouse click response
            Camera[] allCams = Camera.allCameras;
            int i = 0;
            bool inOtherViewport = false;
            while ((!inOtherViewport) && (i < allCams.Length)) 
            {
                if (c != allCams[i]) {
                    viewportPt = allCams[i].ScreenToViewportPoint(Input.mousePosition);
                    inOtherViewport = ((viewportPt.x > 0) && (viewportPt.x < 1.0) && (viewportPt.y > 0) && (viewportPt.y < 1.0));
                }
                i++;
            }
            if (inOtherViewport)
                return false;
        }
        viewportPt = c.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log("Unity viewportPt:" + viewportPt);
        viewportPt = MyScreenToViewport(Input.mousePosition);
        Debug.Log("   my viewportPt:" + viewportPt);
        return ((viewportPt.x > 0) && (viewportPt.x < 1.0) && (viewportPt.y > 0) && (viewportPt.y < 1.0));
    }

}
