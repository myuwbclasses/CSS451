using UnityEngine;
using System.Collections;

public partial class CameraManipulation : MonoBehaviour
{
    public Transform LookAt;
    public bool DrawFrustum = false; 


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
