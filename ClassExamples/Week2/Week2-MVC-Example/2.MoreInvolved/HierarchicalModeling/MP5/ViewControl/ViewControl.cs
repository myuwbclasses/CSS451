#region File Description
//-----------------------------------------------------------------------------
// SpinningTriangleControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// Modified from code at http://create.msdn.com/en-US/education/catalog/sample/winforms_series_1
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

using XNAWinFormLibrary;
using UWB_GraphicsLibrary;

namespace ClassExample
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public partial class ViewControl : GraphicsDeviceControl
    {
        //reference to the model
        protected MyModel mTheWorld = null;
        public MyModel Model { get { return mTheWorld; } set { mTheWorld = value; } }

        /// The camera
        protected Camera mCamera = new Camera();
        public Camera ViewCamera { get { return mCamera; } }

        private MatrixStack mTheStack;

        public ViewControl() { }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // initialize camera parameters
            mCamera.CameraPosition = new Vector3(0, 15f, 15f);
            mCamera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
                // drawing area aspect ratio

            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // For intiailizing mouse, if subclass so desires
            InitMouseSupport();

            mTheStack = new MatrixStack();
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            // Clear background
            GraphicsDevice.Clear(Color.CornflowerBlue);


            DrawViewSpecific();

            // Now draw the world!
            mTheWorld.Draw(GraphicsDevice, GraphicsContentManager, ViewCamera);
        }

        #region give each ViewControl types the ability to draw specifics
        virtual protected void DrawViewSpecific()
        {
        }
        #endregion 
    }
}
