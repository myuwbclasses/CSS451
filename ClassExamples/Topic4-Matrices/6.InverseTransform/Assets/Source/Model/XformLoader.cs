using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour {

    public enum ShowXForm
    {
        ShowInvT,
        ShowInvRT,
        ShowInvSRT,
        ShowTRS
    };

    public ShowXForm ToShow = ShowXForm.ShowTRS;
    public Transform RefBlueTree = null;

    public float DeltaPosition = 0f;


    // Use this for initialization
    void Start() {
        
    }

    void Update() { 
        Material mat = GetComponent<Renderer>().material;
        Matrix4x4 TRS = Matrix4x4.TRS(RefBlueTree.transform.localPosition, RefBlueTree.transform.localRotation, RefBlueTree.transform.localScale);

        // this is lousy efficiency, but, hopefully clearer

        // inverse translation
        Matrix4x4 invT = Matrix4x4.Translate(-RefBlueTree.transform.localPosition);  // notice the negative sign

        // inverse rotation
        float rotAngle = 0f;
        Vector3 rotAxis = Vector3.right;
        RefBlueTree.transform.localRotation.ToAngleAxis(out rotAngle, out rotAxis);
        Matrix4x4 invR = Matrix4x4.Rotate(Quaternion.AngleAxis(-rotAngle, rotAxis));      // Again, the negative sign

        // inverse scaling: assuming no zeros!!
        Vector3 s = new Vector3(1.0f / RefBlueTree.transform.localScale.x, 
                                1.0f / RefBlueTree.transform.localScale.y, 
                                1.0f / RefBlueTree.transform.localScale.z);
        Matrix4x4 invS = Matrix4x4.Scale(s);
    
        switch (ToShow)
        {
            case ShowXForm.ShowInvT:  // The Green one
                Matrix4x4 TRS_invT = Matrix4x4.Translate(new Vector3(1.4f*DeltaPosition, 0f, 0f)) * invT * TRS;
                mat.SetMatrix("MyXformMat", TRS_invT);
                break;

            case ShowXForm.ShowInvRT: // The Red one
                Matrix4x4 TRS_invRT = Matrix4x4.Translate(new Vector3(DeltaPosition, 0f, -DeltaPosition)) * invR * invT * TRS;
                mat.SetMatrix("MyXformMat", TRS_invRT);
                break;

            case ShowXForm.ShowInvSRT:  // The White one
                Matrix4x4 TRS_invSRT = Matrix4x4.Translate(new Vector3(0f, 0f, -1.4f * DeltaPosition)) * invS * invR * invT * TRS;
                mat.SetMatrix("MyXformMat", TRS_invSRT);
                break;

            case ShowXForm.ShowTRS:
                mat.SetMatrix("MyXformMat", TRS);
                break;
        }
    }
}