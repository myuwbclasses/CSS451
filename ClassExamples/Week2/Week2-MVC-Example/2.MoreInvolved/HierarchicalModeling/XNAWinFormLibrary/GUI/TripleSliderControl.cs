using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace XNAWinFormLibrary
{
    public partial class TripleSliderControl : UserControl
    {

        // Register call back functions in the form:
        //          void CallBack(float newValue) 
        // To receive call backs when slider values are changed.
        //
        // Look for example of how to use these event handlers under in
        //
        //     ShapeTransformControl.cs
        //
        //  in region defined under:
        //
        //         Radio Button Change
        //
        public event SliderControlWithEcho.OnNewSliderValueDelegate NewTopSliderValue;
        public event SliderControlWithEcho.OnNewSliderValueDelegate NewMidSliderValue;
        public event SliderControlWithEcho.OnNewSliderValueDelegate NewBottomSliderValue;


        /// <summary>
        ///  Constructor
        /// </summary>
        public TripleSliderControl()
        {
            InitializeComponent();
            
            NewTopSliderValue = null;
            NewMidSliderValue = null;
            NewBottomSliderValue = null;

            mTopSlider.OnNewSliderValue += topSliderResponse;
            mMidSlider.OnNewSliderValue += midSliderResponse;
            mBottomSlider.OnNewSliderValue += bottomSliderResponse;
        }

        /// <summary>
        /// Sets the title and label of each of the sliders
        /// </summary>
        /// <param name="title">title of the group (e.g., Transformation)</param>
        /// <param name="topLabel">top label</param>
        /// <param name="midLabel">middle label</param>
        /// <param name="bottomLabel">bottom label</param>
        public void SetTitleLabel(String title, String topLabel, String midLabel, String bottomLabel)
        {
            mTitle.Text = title;
            mTopSlider.SetLabel(topLabel);
            mMidSlider.SetLabel(midLabel);
            mBottomSlider.SetLabel(bottomLabel);
        }

        /// <summary>
        /// Sets the slider bar range
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        public void SetSliderRange(float min, float max)
        {
            mTopSlider.ChangeRangeValues(min, max, (min + max) / 2);
            mMidSlider.ChangeRangeValues(min, max, (min + max) / 2);
            mBottomSlider.ChangeRangeValues(min, max, (min + max) / 2);
        }

        /// <summary>
        /// Sets the slider bar values
        /// </summary>
        /// <param name="topValue">top slider bar value</param>
        /// <param name="midValue">mid slider bar value</param>
        /// <param name="bottomValue">bottom slider bar value</param>
        public void SetSliders(float topValue, float midValue, float bottomValue)
        {
            mTopSlider.SetSliderValue(topValue);
            mMidSlider.SetSliderValue(midValue);
            mBottomSlider.SetSliderValue(bottomValue);
        }

        #region defult response functions for the SliderControlWithEcho with new sldier values
        private void topSliderResponse(float newValue)
        {
            if (null != NewTopSliderValue)
                NewTopSliderValue(newValue);
        }
        private void midSliderResponse(float newValue)
        {
            if (null != NewMidSliderValue)
                NewMidSliderValue(newValue);
        }
        private void bottomSliderResponse(float newValue)
        {
            if (null != NewBottomSliderValue)
                NewBottomSliderValue(newValue);
        }
        #endregion
    }
}
