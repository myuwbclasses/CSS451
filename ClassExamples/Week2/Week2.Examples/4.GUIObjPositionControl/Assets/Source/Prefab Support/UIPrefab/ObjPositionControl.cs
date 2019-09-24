using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjPositionControl : MonoBehaviour {

    public SliderWithEcho XSlider = null;
    public SliderWithEcho ZSlider = null;

    private GameObject mSelected = null;
    
	// Use this for initialization
	void Start () {
        Debug.Assert(XSlider != null);
        Debug.Assert(ZSlider != null);

        XSlider.SetSliderListener(NewXValue);
        ZSlider.SetSliderListener(NewZValue);
    }

    void Update()
    {
        ObjSetUI();
    }

    public void SetSelectedObj(GameObject g)
    {
        mSelected = g;
        ObjSetUI();
    }

    // Obj set UI
    void ObjSetUI()
    {
        if (mSelected != null)
        {
            Vector3 p = mSelected.transform.localPosition;
            XSlider.SetSliderValue(p.x);
            ZSlider.SetSliderValue(p.z);
        }
    }

    // GUI set object
    void NewXValue(float nx)
    {
        if (mSelected != null)
        {
            Vector3 p = mSelected.transform.localPosition;
            p.x = nx;
            mSelected.transform.localPosition = p;
        }
    }

    void NewZValue(float nz)
    {
        if (mSelected != null)
        {
            Vector3 p = mSelected.transform.localPosition;
            p.z = nz;
            mSelected.transform.localPosition = p;
        }
    }
}
