using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public SliderWithEcho PX = null;
    public SliderWithEcho PZ = null;
    

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
        Debug.Assert(PX != null);
        Debug.Assert(PZ != null);

        PX.InitSliderRange(-11f, 11f, 0f);
        PZ.InitSliderRange(-11f, 11f, 0f);
        
        PX.SetSliderListener(NewVXValue);
        PZ.SetSliderListener(NewVZValue);
    }

    // Update is called once per frame
    void Update() {
        ProcessMouseEvents();

        if (TheWorld.HasSelected())
        {
            Vector3 p = TheWorld.GetSelectedPosition();
            PX.SetSliderValue(p.x);
            PZ.SetSliderValue(p.z);
        }

    }

    // UI sets Model
    void NewVXValue(float x)
    {
        TheWorld.SetSelectedPosition(x, PZ.GetSliderValue());
    }

    void NewVZValue(float z)
    {
        TheWorld.SetSelectedPosition(PX.GetSliderValue(), z);
    }

}