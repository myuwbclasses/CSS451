using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using XNAWinFormLibrary;
using UWB_GraphicsLibrary;

namespace ClassExample
{
    partial class XformInfoControl : TripleSliderControl
    {
        private XformInfo mTheXform;
        private Vector3 mCurrentRotateValues;
                
        /// <summary>
        /// Constructor
        /// </summary>
        public XformInfoControl() : base()
        {
            InitializeComponent();
            SetTitleLabel("Transform", "X", "Y", "Z");

            mT.Checked = true;
            mTheXform = null;

            // to keep track of the values on the sldier bar
            mCurrentRotateValues = Vector3.Zero;
        }

        /// <summary>
        /// Sets the XformSetsUI upon which 
        /// the transform will be controlled by this GUI object
        /// </summary>
        /// <param name="t">The XformInfo who's transform to be control</param>
        public void SetXform(XformInfo t)
        {
            mTheXform = t;
            XformSetsUI();
        }

        #region use XformSetsUI values to set the UI slider bars
        private void XformSetsUI()
        {
            if (null == mTheXform)
                return;
            Vector3 m;
            if (mT.Checked)
            {
                m = mTheXform.Translation;
            }
            else if (mR.Checked)
            {
                mCurrentRotateValues = Vector3.Zero;
                m = mCurrentRotateValues;
            }
            else if (mP.Checked)
            {
                m = mTheXform.Pivot;
            }
            else if (mS.Checked)
            {
                m = mTheXform.Scale;
            }
            else
            {
                m = Vector3.Zero;
            }

            SetSliders(m.X, m.Y, m.Z);
        }
        #endregion

        #region Radio Button changes
        private void mT_CheckedChanged(object sender, EventArgs e)
        {
            if (mT.Checked)
            {
                SetSliderRange(-40, +30);
                NewTopSliderValue += UISetShapeTranslationX;
                NewMidSliderValue += UISetShapeTranslationY;
                NewBottomSliderValue += UISetShapeTranslationZ;
                XformSetsUI();
            }
            else
            {
                NewTopSliderValue -= UISetShapeTranslationX;
                NewMidSliderValue -= UISetShapeTranslationY;
                NewBottomSliderValue -= UISetShapeTranslationZ;
            }
        }

        private void mR_CheckedChanged(object sender, EventArgs e)
        {
            if (mR.Checked)
            {
                SetSliderRange(-3.15f, +3.15f);
                NewTopSliderValue += UISetShapeRotationX;
                NewMidSliderValue += UISetShapeRotationY;
                NewBottomSliderValue += UISetShapeRotationZ;
                XformSetsUI();
            }
            else
            {
                NewTopSliderValue -= UISetShapeRotationX;
                NewMidSliderValue -= UISetShapeRotationY;
                NewBottomSliderValue -= UISetShapeRotationZ;
            }
        }

        private void mS_CheckedChanged(object sender, EventArgs e)
        {
            if (mS.Checked)
            {
                SetSliderRange(0.1f, +10);
                NewTopSliderValue += UISetShapeScaleX;
                NewMidSliderValue += UISetShapeScaleY;
                NewBottomSliderValue += UISetShapeScaleZ;
                XformSetsUI();
            }
            else
            {
                NewTopSliderValue -= UISetShapeScaleX;
                NewMidSliderValue -= UISetShapeScaleY;
                NewBottomSliderValue -= UISetShapeScaleZ;
            }
        }

        private void mP_CheckedChanged(object sender, EventArgs e)
        {
            if (mP.Checked)
            {
                SetSliderRange(-20, +20);
                NewTopSliderValue += UISetShapePivotX;
                NewMidSliderValue += UISetShapePivotY;
                NewBottomSliderValue += UISetShapePivotZ;
                XformSetsUI();
            }
            else
            {
                NewTopSliderValue -= UISetShapePivotX;
                NewMidSliderValue -= UISetShapePivotY;
                NewBottomSliderValue -= UISetShapePivotZ;
            }
        }
        #endregion 

        #region Translation Response Functions for UI slider changes
        private void UISetShapeTranslationX(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 t = mTheXform.Translation;
            t.X = newValue;
            mTheXform.Translation = t;
        }

        private void UISetShapeTranslationY(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 t = mTheXform.Translation;
            t.Y = newValue;
            mTheXform.Translation = t;
        }

        private void UISetShapeTranslationZ(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 t = mTheXform.Translation;
            t.Z = newValue;
            mTheXform.Translation = t;
        }
        #endregion 

        #region Scale Response Functions for UI slider changes
        private void UISetShapeScaleX(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 s = mTheXform.Scale;
            s.X = newValue;
            mTheXform.Scale = s;
        }

        private void UISetShapeScaleY(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 s = mTheXform.Scale;
            s.Y = newValue;
            mTheXform.Scale = s;
        }

        private void UISetShapeScaleZ(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 s = mTheXform.Scale;
            s.Z = newValue;
            mTheXform.Scale = s;
        }
        #endregion 

        #region Rotation response functions for UI slider changes

        private void UISetShapeRotationX(float newValue)
        {
            if (null == mTheXform)
                return;
            mTheXform.RotateInXByRadian(newValue-mCurrentRotateValues.X);
            mCurrentRotateValues.X = newValue;
        }

        private void UISetShapeRotationY(float newValue)
        {
            if (null == mTheXform)
                return;
            mTheXform.RotateInYByRadian(newValue - mCurrentRotateValues.Y);
            mCurrentRotateValues.Y = newValue;
        }

        private void UISetShapeRotationZ(float newValue)
        {
            if (null == mTheXform)
                return;
            mTheXform.RotateInZByRadian(newValue - mCurrentRotateValues.Z);
            mCurrentRotateValues.Z = newValue;
        }
        #endregion

        #region Pivot Response Functions for UI slider changes
        private void UISetShapePivotX(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 p = mTheXform.Pivot;
            p.X = newValue;
            mTheXform.Pivot = p;
        }

        private void UISetShapePivotY(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 p = mTheXform.Pivot;
            p.Y = newValue;
            mTheXform.Pivot = p;
        }

        private void UISetShapePivotZ(float newValue)
        {
            if (null == mTheXform)
                return;
            Vector3 p = mTheXform.Pivot;
            p.Z = newValue;
            mTheXform.Pivot = p;
        }
        #endregion 

        private void Reset_Click(object sender, EventArgs e)
        {
            if (null != mTheXform)
            {
                mTheXform.ToIdentity();
                XformSetsUI();
            }
        }
    }
}
