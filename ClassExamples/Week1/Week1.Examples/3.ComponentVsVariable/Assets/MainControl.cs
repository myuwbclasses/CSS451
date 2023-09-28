using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour {

    // Parmeters to control MoveInX
    public MoveInX mScript;
    private int count = 0;

    // Reference to a GameObject
    public GameObject mObject;

    // These are the two buttons
    public Button mDeleteScript;
    public Button mCreateComponent;
    public Button mDeleteObject;

	// Use this for initialization
	void Start () {
        Debug.Assert(mDeleteScript != null);
        Debug.Assert(mDeleteObject != null);
        Debug.Assert(mCreateComponent != null);

        mDeleteScript.onClick.AddListener(DeleteComponent);
        mDeleteObject.onClick.AddListener(DeleteGameObject);
        mCreateComponent.onClick.AddListener(CreateComponent);

	}
	
	// Update is called once per frame
	void Update () {
        if (mScript== null)
            return;

        count++;
        if (count > 100) {
            mScript.IncDelta();
            count = -100;
        }
        else if (count == 0)
        {
            mScript.DecDelta();
        }
	}

    // Button service functions
    void DeleteComponent()
    {
        if ((mScript == null) && (mObject != null))
            mScript = mObject.GetComponent<MoveInX>();

        if (mScript != null)
            Destroy(mScript);  // this will only take effect _AFTER_ this update cycle

        mScript = null;
        
    }

    void DeleteGameObject()
    {
        Destroy(mObject);
        mObject = null;
    }

    void CreateComponent()
    {
        if (mObject == null) {  // must have deleted, re-create
            mObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        mScript = mObject.AddComponent<MoveInX>();  // what happens when we have more than one?
    }
}
