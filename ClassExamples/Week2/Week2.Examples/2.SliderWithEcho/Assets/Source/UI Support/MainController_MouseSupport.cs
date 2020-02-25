using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // Mouse click selection 
    // Copied from: http://answers.unity3d.com/questions/411793/selecting-a-game-object-with-a-mouse-click-on-it.html
    void ProcessMouseEvents()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            // Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
                        
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
                        // 1 is the mask for default layer
            if (hit)
            {
                TheWorld.SelectObjectAt(hitInfo.transform.gameObject, hitInfo.point);
            }
            TheSlider.SetSliderValue(TheWorld.GetSelectedRadius());  
        } 
    }
}
