using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectControl : MonoBehaviour
{
    private TextMeshProUGUI mUILabel = null;
    private Button mDeleteButton = null;
    private Slider mObjectSizeSlider = null;

    private GameObject mObjectToControl = null;


    // Use this for initialization
    void Start()
    {
        // let's find all the GUI elements
        mUILabel = transform.Find("GroupLabel").GetComponent<TextMeshProUGUI>();
        mDeleteButton = transform.Find("DeleteButton").GetComponent<Button>();
        mObjectSizeSlider = transform.Find("SizeControl").GetComponent<Slider>();

        mDeleteButton.onClick.AddListener(DeleteSelected);
        mObjectSizeSlider.onValueChanged.AddListener(ResizeSelected);

        mUILabel.text = "No Object Selected";
        gameObject.SetActive(false);
    }


    public void SetSelectedObject(GameObject g)
    {
        if (mObjectToControl != null)
        {
            SetSelectedColor(1f);
        }
        gameObject.SetActive(true);
        mObjectToControl = g;
        // mObjectSizeSlider.value = g.transform.localScale.x; // assume the xyz are the same.
        mUILabel.text = g.name;
        SetSelectedColor(0f);
    }

    void DeleteSelected()
    {
        if (mObjectToControl != null)
        {
            Destroy(mObjectToControl);
            mObjectToControl = null;
            gameObject.SetActive(false);
        }
    }

    void ResizeSelected(float newValue)
    {
        if (mObjectToControl != null)
        {
            mObjectToControl.transform.localScale = new Vector3(newValue, newValue, newValue);
        }
    }

    void SetSelectedColor(float b)
    {
        Material m = mObjectToControl.GetComponent<Renderer>().material;
        Color c = m.color;
        c.b = b;
        m.color = c;
    }
}