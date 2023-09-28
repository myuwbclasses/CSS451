using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {

    void ProcessMouseEvents()
    {
        GameObject selectedObj;
        Vector3 hitPoint;

        if (EventSystem.current.IsPointerOverGameObject())
            return; // ignore mouse when it is over EventSystem's objects
            
        if (Input.GetMouseButtonDown(0)) // Click event
        {
            if (MouseSelectObjectAt(out selectedObj, out hitPoint, LayerMask.GetMask("Default")))
            {
                TheWorld.CreateBallAt(hitPoint);
            }
            
        } else if (Input.GetMouseButton(0)) // Mouse Drag
        {
            if (MouseSelectObjectAt(out selectedObj, out hitPoint, 1<<0)) // Notice the two ways of getting the mask
            {
                TheWorld.ResizeCreatedTo(hitPoint);
            }   
        } else if (Input.GetMouseButtonUp(0)) // Mouse Release
        {
            TheWorld.EnableCreatedMotion();
        }

        if (Input.GetMouseButtonDown(1))  // RMB
        {
            if (MouseSelectObjectAt(out selectedObj, out hitPoint, 1<<8))  // or LayerMask.GetMask("Spheres")
            {
                TheWorld.SetSelected(ref selectedObj);
            }
        }
    }

    bool MouseSelectObjectAt(out GameObject g, out Vector3 p, int layerMask)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, layerMask);
        // Debug.Log("MouseSelect:" + layerMask + " Hit=" + hit);
        if (hit)
        {
            g = hitInfo.transform.gameObject;
            p = hitInfo.point;
        } else
        {
            g = null;
            p = Vector3.zero;
        }
        return hit;
    }
}
