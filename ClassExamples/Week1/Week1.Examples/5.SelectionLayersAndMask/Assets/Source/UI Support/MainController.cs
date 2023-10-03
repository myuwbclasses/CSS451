using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Button CreateButton = null;
    public Button DeleteSelectedButton = null;
    public Toggle ShowSphereToggle = null;
    public Camera MainCamera = null;

    // information that will be changed by user action (identify selected)
    private GameObject mSelectedObject = null;
    private GameObject mTheSphere = null;

	// Use this for initialization
	void Start () {
        Debug.Assert(CreateButton != null);
        Debug.Assert(DeleteSelectedButton != null);
        Debug.Assert(ShowSphereToggle != null);
        Debug.Assert(MainCamera != null);

        // Add listeners to the button
        CreateButton.onClick.AddListener(CreateNewCube);
        DeleteSelectedButton.onClick.AddListener(DeleteSelectedCube);
        ShowSphereToggle.onValueChanged.AddListener(ShowSphere);

        // now initialize the sphere reference
        mTheSphere = GameObject.Find("MySphere");
        Debug.Assert(mTheSphere != null);

        // Initialize layermask ...
        LayerMask = UnityEngine.LayerMask.GetMask("Default");
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

    void DeleteSelectedCube()
    {
        if (mSelectedObject != null)
        {
            Destroy(mSelectedObject);  // delete this object
            mSelectedObject = null;
        }

    }

    void ShowSphere(bool newValue)
    {
        mTheSphere.GetComponent<Renderer>().enabled = newValue;
    }
}
