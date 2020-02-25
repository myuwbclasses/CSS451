using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Button CreateButton = null;
    public Camera MainCamera = null;

    public ObjectControl ObjectControl = null;

	// Use this for initialization
	void Start () {
        Debug.Assert(CreateButton != null);
        Debug.Assert(MainCamera != null);
        Debug.Assert(ObjectControl != null);

        // Add listeners to the button
        CreateButton.onClick.AddListener(CreateNewCube);
	}
	
	// Update is called once per frame
	void Update () {
        LMBSelect();
	}

    // UI support functions
    void CreateNewCube()
    {
        GameObject cube = Instantiate(Resources.Load("MyCube")) as GameObject;
            // NOTE: Resources.Load("file path to the prefab") so, in this case
            //       MyCube.prefab MUST be located in Resoruces/ folder
        cube.transform.position = new Vector3(10, 0, 10);
    }
}
