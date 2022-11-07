using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class TheWorld : MonoBehaviour  {

    public SceneNode TheRoot;
    public Transform RootOrg;
    public SceneNode TheChild;
    public Transform ChildOrg;
    public SceneNode TheFront;
    public Transform FrontOrg;
    public Transform FrontTip;      // This is now the target position

    public bool TrackTarget = false;
    public bool RotateRoot = false;
    public float RootDelta = 0.2f;

    public bool RotateChild = false;
    public float ChildDelta = 0.5f;

    private float FrontHeight = 8.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        //TheRoot.OrbitAroundWorldY(0.4f); // degree
        UpdateHierarchy();

        if (TrackTarget) {            
            // do root
            if (RotateRoot) {    
                Vector3 rootDir = FrontTip.localPosition - RootOrg.localPosition;
                TheRoot.RotateUpTowardsBy(rootDir, RootDelta);
                UpdateHierarchy();
            }

            if (RotateChild) {
                Vector3 childDir = FrontTip.localPosition - ChildOrg.localPosition;
                TheChild.RotateUpTowardsBy(childDir, ChildDelta);
                UpdateHierarchy();
            }

            Vector3 dir = FrontTip.localPosition - FrontOrg.localPosition;
            TheFront.AlignUpWith(dir);
            UpdateHierarchy();

            // FrontTip.localPosition = FrontOrg.localPosition + FrontHeight * FrontOrg.up;
            FrontTip.localRotation = FrontOrg.localRotation;
        }
    }

    private void UpdateHierarchy() {
        
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);

        TheRoot.SetAxisFrame(RootOrg);
        TheChild.SetAxisFrame(ChildOrg);
        TheFront.SetAxisFrame(FrontOrg);
    }
}
