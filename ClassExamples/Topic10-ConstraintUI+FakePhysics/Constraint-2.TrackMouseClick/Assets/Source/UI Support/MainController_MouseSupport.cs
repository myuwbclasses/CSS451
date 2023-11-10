using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {
   
    private const int kPlaneLayer = (1 << 8);        // 1<<8 is the mask for WallLayer (8)
    public Transform mSelectionPoint = null;


    // Mouse click selection 
    private void LMBService()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            // Debug.Log("Mouse is down");
            RaycastHit hitInfo = new RaycastHit();

            // now try to hit end points ...
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, kPlaneLayer))
            {
                mSelectionPoint.localPosition = hitInfo.point;
                // This is not good (or acceptable) programming style, but, 
                // for easy to follow code, let's do this
                TheWorld.FrontTip.localPosition = hitInfo.point;
            }
            
        }   
    }
}
