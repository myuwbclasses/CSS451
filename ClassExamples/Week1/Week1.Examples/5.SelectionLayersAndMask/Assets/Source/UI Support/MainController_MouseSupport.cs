using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using TMPro; // this is TextMeshPro
using UnityEngine.EventSystems;  // 

public partial class MainController : MonoBehaviour {

    int LayerMask = 0; // this is the default

    // Mouse click selection 
    // Copied from: http://answers.unity3d.com/questions/411793/selecting-a-game-object-with-a-mouse-click-on-it.html
    void LMBSelect()
    {
        // Debug.Log("Over GameObject:" + EventSystem.current.IsPointerOverGameObject());
        if ((!EventSystem.current.IsPointerOverGameObject())   // only try to select an object if pointer is NOT 
                                                               // over an event system object 
                &&  // 
            Input.GetMouseButtonDown(0) 
            )
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 
                Mathf.Infinity,   // allow the ray to travel to infinity for intersection
                LayerMask);         // only select objects in this layer
            if (hit)
            {
                if (mSelectedObject != null)
                {
                    SetSelectedColor(1f);
                }
                mSelectedObject = hitInfo.transform.gameObject;
                Debug.Log("Hit " + mSelectedObject.name);
                SetSelectedColor(0f);
            }
            else {
                Debug.Log("No hit");
            }
        }
    }

    void SetSelectedColor(float b)
    {
        Material m = mSelectedObject.GetComponent<Renderer>().material;
        Color c = m.color;
        c.b = b;
        m.color = c;
    }

    
    public void LayerSelectionChange(TMP_Dropdown d)
    {
        Debug.Log("Seleciton: V=" + d.value);
        // in the Editor:
        // Sphere: item-1 in the Dropdown
        //         Sphere Layer is 6
        // Cylinder: item-2 in the Dropdown
        //         Cylinder Layer is 7
        // Plane: item-3 in the Dropdown
        //         Plane Layer is 8
        switch (d.value)
        {
            case 0:
                LayerMask = UnityEngine.LayerMask.GetMask("Default");
                break;
            case 1:
                LayerMask = UnityEngine.LayerMask.GetMask("Sphere");  // Name must be the same as in the Editor
                break;
            case 2:
                LayerMask = UnityEngine.LayerMask.GetMask("Cylinder");
                break;
            case 3:
                LayerMask = UnityEngine.LayerMask.GetMask("Plane");
                break;
        }
    }

}
