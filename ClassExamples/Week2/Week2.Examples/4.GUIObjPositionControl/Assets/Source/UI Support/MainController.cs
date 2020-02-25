using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public ObjPositionControl PosControl = null;
    

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
        Debug.Assert(PosControl != null);
    }

    // Update is called once per frame
    void Update() {
        ProcessMouseEvents();
    }
}