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
    /// default camera UI for the ViewControl
    /// </summary>
    partial class ViewControl
    {
        private float mCurrentX, mCurrentY;
        private const float kMouseScaleFactor = 0.01f;

        protected virtual void InitMouseSupport()
        {
            // mouse 
            MouseMove += new MouseEventHandler(ViewControl_MouseMove);
            MouseDown += new MouseEventHandler(ViewControl_MouseDown);
            MouseUp += new MouseEventHandler(ViewControl_MouseUp);

            mCurrentX = 0f;
            mCurrentY = 0f;
        }

        protected void ViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            mCurrentX = e.X;
            mCurrentY = e.Y;
            MainWindow.EchoToStatus("Mouse down: " + e.Button.ToString() + ":" + e.X + " " + e.Y);
        }

        protected void ViewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ( (Control.ModifierKeys & Keys.Shift) == 0)
                return;

            float dx = (e.X - mCurrentX) * kMouseScaleFactor;
            float dy = (e.Y - mCurrentY) * kMouseScaleFactor;

            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    ViewCamera.RotateCameraAboutUp(dx);
                    ViewCamera.RotateCameraAboutSide(dy);
                    break;

                case System.Windows.Forms.MouseButtons.Right:
                    float d = dx + dy;
                    ViewCamera.ZoomCamera(d);
                    break;

                case System.Windows.Forms.MouseButtons.Middle:
                    ViewCamera.PanCamera(10*dx, 10*dy);
                    break;
            }
            mCurrentX = e.X;
            mCurrentY = e.Y;
            MainWindow.EchoToStatus("Mouse move: " + e.Button.ToString() + ":" + e.X + " " + e.Y);
        }

        protected void ViewControl_MouseUp(object sender, MouseEventArgs e)
        {
            MainWindow.EchoToStatus("Mouse up: " + e.Button.ToString() + ":" + e.X + " " + e.Y);
        }
    }
}
