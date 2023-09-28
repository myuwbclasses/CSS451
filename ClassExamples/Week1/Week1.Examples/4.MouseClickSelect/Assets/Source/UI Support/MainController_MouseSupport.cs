using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using TMPro; // this is TextMeshPro

public partial class MainController : MonoBehaviour {

    // Mouse click selection 
    // Copied from: http://answers.unity3d.com/questions/411793/selecting-a-game-object-with-a-mouse-click-on-it.html
    void LMBSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
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


}
