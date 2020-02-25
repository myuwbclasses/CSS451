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
    partial class ShapeColorControl : TripleSliderControl
    {
        private Shape mTheShape;
                
        /// <summary>
        /// Constructor
        /// </summary>
        public ShapeColorControl() : base()
        {
            InitializeComponent();
            SetTitleLabel("Shape Color", "R", "G", "B");
            SetSliderRange(0, 255);
            NewTopSliderValue += UISetShapeColorR;
            NewMidSliderValue += UISetShapeColorG;
            NewBottomSliderValue += UISetShapeColorB;

            mTheShape = null;
        }

        /// <summary>
        /// Sets the shape upon which 
        /// the transform will be controlled by this GUI object
        /// </summary>
        /// <param name="s">The Shape who's transform to be control</param>
        public void SetShape(Shape s)
        {
            mTheShape = s;
            ShapeSetsUI();
        }

        #region use Shape values to set the UI slider bars
        private void ShapeSetsUI()
        {
            if (null == mTheShape)
                return;
            Color c = mTheShape.Color;
            SetSliders(c.R, c.G, c.B);
        }
        #endregion

        #region Response Functions for UI slider changes
        private void UISetShapeColorR(float newValue)
        {
            if (null == mTheShape)
                return;
            Color c = mTheShape.Color;
            c.R = (Byte) newValue;
            mTheShape.Color = c;
        }
        private void UISetShapeColorG(float newValue)
        {
            if (null == mTheShape)
                return;
            Color c = mTheShape.Color;
            c.G = (Byte)newValue;
            mTheShape.Color = c;
        }
        private void UISetShapeColorB(float newValue)
        {
            if (null == mTheShape)
                return;
            Color c = mTheShape.Color;
            c.B = (Byte)newValue;
            mTheShape.Color = c;
        }
        #endregion 

    }
}
