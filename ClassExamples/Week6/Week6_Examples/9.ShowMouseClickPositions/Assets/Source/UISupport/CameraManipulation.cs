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

        // Check to make sure mouse position is indeed in the viewport
        if (!MouseInCameraViewport())
            return;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            ShowScreenToWorld(Input.mousePosition);
        }
    }

    public void SetLookAtPos(Vector3 p)
    {
        LookAt.localPosition = p;
        transform.LookAt(p);
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
