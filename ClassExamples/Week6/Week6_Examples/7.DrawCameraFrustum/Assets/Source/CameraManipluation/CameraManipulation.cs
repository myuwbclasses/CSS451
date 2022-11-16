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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrustumPosition();
    }

}
