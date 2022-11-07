using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {
    public Transform mPointToTrack = null;
    public Transform mNodeOrg = null;  // this is the node origin


    // Mouse click selection 
    private void ProcessMouseEvents()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            
            float d = UpdatePointToTrack(ref ray);
        }   
    }

     // Return: projects mNodeOrg on to the Ray at mPointToTrack
    private float UpdatePointToTrack(ref Ray r)
    {
        Vector3 va = mNodeOrg.localPosition - r.origin;
        float h = Vector3.Dot(va, r.direction);

        float d = 0f;
        mPointToTrack.localPosition = Vector3.zero;

        if (h < 0) { 
            d = -1; // not valid
        } else
        {
            d = Mathf.Sqrt(va.sqrMagnitude - h * h);
            mPointToTrack.localPosition = r.origin + h * r.direction;
        }
        return d;
    }
}
