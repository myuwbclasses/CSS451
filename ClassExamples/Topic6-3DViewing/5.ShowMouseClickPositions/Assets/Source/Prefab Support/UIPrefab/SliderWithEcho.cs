using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWithEcho : MonoBehaviour {

    public Slider TheSlider = null;
    public Text TheEcho = null;
    public Text TheLabel = null;

    public delegate void SliderCallbackDelegate(float v);      // defined a new type
    private SliderCallbackDelegate mCallBack = null;            // 


	// Use this for initialization
	void Start () {
        Debug.Assert(TheSlider != null);
        Debug.Assert(TheEcho != null);
        Debug.Assert(TheLabel != null);

        TheSlider.onValueChanged.AddListener(SliderValueChange);
    }

    public void SetSliderListener(SliderCallbackDelegate listener)
    {
        mCallBack = listener;
    }
	
	void SliderValueChange(float v)
    {
        UpdateEcho();
        // Debug.Log("SliderValueChange: " + v);
        if (mCallBack != null)
            mCallBack(v);
    }

    public float GetSliderValue() { return TheSlider.value; }

    public void SetSliderLabel(string l) { TheLabel.text = l; }
    public void SetSliderValue(float v) {
        TheSlider.value = v;
        UpdateEcho();
    }
    public void InitSliderRange(float min, float max, float v)
    {
        TheSlider.minValue = min;
        TheSlider.maxValue = max;
        //SliderCallbackDelegate c = mCallBack;
        //mCallBack = null;   // to make sure no call back triggered
            SetSliderValue(v);
        // mCallBack = c;
    }

    private void UpdateEcho()
    {
        if (TheSlider.wholeNumbers)
            TheEcho.text = TheSlider.value.ToString("0");
        else
            TheEcho.text = TheSlider.value.ToString("0.0000");

    }

}
