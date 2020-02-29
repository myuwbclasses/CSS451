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
    /// Mouse supporting view control for square re-sizin in this example.
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    class ViewControlWithMouse : ViewControl
    {
        private float mCurrentX, mCurrentY;
        private const float kMouseScaleFactor = 0.1f;

        // We have the ability to visualize the camera in another view
        Camera mOtherViewCamera;
        public void SetOtherViewCamera(Camera viewCamera) { mOtherViewCamera = viewCamera; }

        // The ability to visualize the other view camera
        CameraVisualization mVisualizeCamera;
        MatrixStack mStack; // to support drawing

        protected override void Initialize()
        {
            base.Initialize();
            mCamera.CameraPosition = new Vector3(30f, 30f, 30f);

            mVisualizeCamera = new CameraVisualization();
            mStack = new MatrixStack();
        }


        #region give each ViewControl types the ability to draw specifics
        override protected void DrawViewSpecific()
        {
            mVisualizeCamera.UpdateWithCameraParameters(mOtherViewCamera);
            mVisualizeCamera.Draw(GraphicsDevice, GraphicsContentManager, ViewCamera, mStack);

            mTheWorld.DrawCameraPos(GraphicsDevice, GraphicsContentManager, ViewCamera);
        }
        #endregion 

        #region Mouse support
        protected override void InitMouseSupport()
        {
            MouseMove += new MouseEventHandler(ViewControl_MouseMove);
            MouseDown += new MouseEventHandler(ViewControl_MouseDown);
            MouseUp += new MouseEventHandler(ViewControl_MouseUp);

            mCurrentX = 0f;
            mCurrentY = 0f;
        }

        void ViewControlWithMouse_MouseDown(object sender, MouseEventArgs e)
        {
            mCurrentX = e.X;
            mCurrentY = e.Y;

            // base class takes cares of camera!!
            ViewControl_MouseDown(sender, e);
        }

        void ViewControlWithMouse_MouseMove(object sender, MouseEventArgs e)
        {
            // base class takes care of camrea!!
            ViewControl_MouseMove(sender, e);

            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    mCurrentX = e.X;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    mCurrentX = e.X;
                    break;
            }
        }

        void ViewControlWithMouse_MouseUp(object sender, MouseEventArgs e)
        {
            ViewControl_MouseUp(sender, e);
        }
        #endregion 

    }
}
