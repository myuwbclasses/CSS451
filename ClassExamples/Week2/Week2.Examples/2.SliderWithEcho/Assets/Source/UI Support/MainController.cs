using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public SliderWithEcho TheSlider = null;
    

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
        Debug.Assert(TheSlider != null);

        // initialize the slider (can be done via UI as well)
        TheSlider.SetSliderLabel("Radius");
        TheSlider.InitSliderRange(1, 10, 3); // min, max, current value
        TheSlider.SetSliderListener(RadiusChanged);
    }

    // Update is called once per frame
    void Update() {
        ProcessMouseEvents();
    }

    void RadiusChanged(float r)
    {
        TheWorld.SetSelelectedRadius(r);
    }
}
