#region File Description
//-----------------------------------------------------------------------------
// GraphicsDeviceControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// Modified from code at http://create.msdn.com/en-US/education/catalog/sample/winforms_series_1
// Modified by: Adita Patil
//              Kelvin Sung 
// Last modified: Jan 2012
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace XNAWinFormLibrary
{

    /// <summary>
    /// Custom control uses the XNA Framework GraphicsDevice to render onto
    /// a Windows Form. Derived classes can override the Initialize and Draw
    /// methods to add their own drawing code.
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    abstract public class GraphicsDeviceControl : Control
    {
        #region Instance Variables

        // This is the abstract connection tothe graphics device
        private GraphicsDeviceService mGraphicsDeviceService;

        // for loading external resources
        private ContentManager mContentManager;
        
        #endregion

        #region Accessors for Graphics and ContentManager
        // Gets a GraphicsDevice that can be used to draw onto this control.
        public GraphicsDevice GraphicsDevice {
            get { return mGraphicsDeviceService.GraphicsDevice; }
        }

        // Gets the ContentManager that knows how to load external resoruces
        public ContentManager GraphicsContentManager { get { return mContentManager; } }
        #endregion

        #region Abstract Methods: these are the only two methods subclass needs to override to draw

        /// <summary>
        /// Derived classes override this to initialize their drawing code.
        /// </summary>
        protected abstract void Initialize();


        /// <summary>
        /// Derived classes override this to draw themselves using the GraphicsDevice.
        /// </summary>
        protected abstract void Draw();


        #endregion     

        #region Initialization

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void OnCreateControl()
        {
            // Don't initialize the graphics device if we are running in the designer.
            if (!DesignMode)
            {
                mGraphicsDeviceService = new GraphicsDeviceService(Handle,
                                                                  ClientSize.Width,
                                                                  ClientSize.Height);
                // Give derived classes a chance to initialize themselves.
                Initialize();

                ContentServiceProvider provider = new ContentServiceProvider();
                provider.SetService(mGraphicsDeviceService);
                mContentManager = new ContentManager(provider, "./Content");
            }

            base.OnCreateControl();
        }

        /// <summary>
        /// Disposes the control.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (mGraphicsDeviceService != null)
            {
                mGraphicsDeviceService.Release(disposing);
                mGraphicsDeviceService = null;
            }

            base.Dispose(disposing);
        }


        #endregion

        #region Paint: include BeginDraw/EndDraw, and OnPaint(that calls Begin/End Draw)

        /// <summary>
        /// Assumes graphics device is properly initialized and draws!
        /// </summary>
        public void RedrawWindow()
        {
            BeginDraw();
            Draw();
            EndDraw();
        }

        /// <summary>
        /// Redraws the control in response to a WinForms paint message.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            string beginDrawError = BeginDraw();

            if (string.IsNullOrEmpty(beginDrawError))
            {
                Draw(); // this is a pure virtual function!
                EndDraw();
            }
            else
            {
                // If BeginDraw failed, show an error message using System.Drawing.
                PaintUsingSystemDrawing(e.Graphics, beginDrawError);
            }
        }

        /// <summary>
        /// Attempts to begin drawing the control. Returns an error message string
        /// if this was not possible, which can happen if the graphics device is
        /// lost, or if we are running inside the Form designer.
        /// </summary>
       protected string BeginDraw()
        {
            // If we have no graphics device, we must be running in the designer.
            if (mGraphicsDeviceService == null)
            {
                return Text + "\n\n" + GetType();
            }

            // Make sure the graphics device is big enough, and is not lost.
            string deviceResetError = HandleDeviceReset();

            if (!string.IsNullOrEmpty(deviceResetError))
            {
                return deviceResetError;
            }

            return null;
        }


        /// <summary>
        /// Ends drawing the control. This is called after derived classes
        /// have finished their Draw method, and is responsible for presenting
        /// the finished image onto the screen, using the appropriate WinForms
        /// control handle to make sure it shows up in the right place.
        /// </summary>
        protected void EndDraw()
        {
            try
            {
                GraphicsDevice.Present(null, null, this.Handle);
            }
            catch
            {
                // Present might throw if the device became lost while we were
                // drawing. The lost device will be handled by the next BeginDraw,
                // so we just swallow the exception.
            }
        }


        /// <summary>
        /// Helper used by BeginDraw. This checks the graphics device status,
        /// making sure it is big enough for drawing the current control, and
        /// that the device is not lost. Returns an error string if the device
        /// could not be reset.
        /// </summary>
        string HandleDeviceReset()
        {
            bool deviceNeedsReset = false;

            switch (GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    // If the graphics device is lost, we cannot use it at all.
                    return "Graphics device lost";

                case GraphicsDeviceStatus.NotReset:
                    // If device is in the not-reset state, we should try to reset it.
                    deviceNeedsReset = true;
                    break;

                default:
                    // If the device state is ok, check whether it is big enough.
                    PresentationParameters pp = GraphicsDevice.PresentationParameters;

                    deviceNeedsReset = (ClientSize.Width > pp.BackBufferWidth) ||
                                       (ClientSize.Height > pp.BackBufferHeight);
                    break;
            }

            // Do we need to reset the device?
            if (deviceNeedsReset)
            {
                try
                {
                    mGraphicsDeviceService.ResetDevice(ClientSize.Width,
                                                      ClientSize.Height);
                }
                catch (Exception e)
                {
                    return "Graphics device reset failed\n\n" + e;
                }
            }

            return null;
        }


        /// <summary>
        /// If we do not have a valid graphics device (for instance if the device
        /// is lost, or if we are running inside the Form designer), we must use
        /// regular System.Drawing method to display a status message.
        /// </summary>
        protected virtual void PaintUsingSystemDrawing(Graphics graphics, string text)
        {
            graphics.Clear(Color.CornflowerBlue);

            using (Brush brush = new SolidBrush(Color.Black))
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    graphics.DrawString(text, Font, brush, ClientRectangle, format);
                }
            }
        }


        /// <summary>
        /// Ignores WinForms paint-background messages. The default implementation
        /// would clear the control to the current background color, causing
        /// flickering when our OnPaint implementation then immediately draws some
        /// other color over the top using the XNA Framework GraphicsDevice.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }


        #endregion

    }

    #region ContentSerivceProvider: to support the creation of ContentManager
    /// <summary>
    /// Dummy container for providing graphics services, use for creating
    /// a ContentManager for loading external resources: images, audio, etc.
    /// Only required for the creation of the ContentManager, that's why private
    /// </summary>
    class ContentServiceProvider : IServiceProvider
    {
        private GraphicsDeviceService mGraphicsDeviceService;

        public void SetService(GraphicsDeviceService s)
        {
            mGraphicsDeviceService = s;
        }

        public object GetService(System.Type serviceType)
        {
            return mGraphicsDeviceService;
        }
    }
    #endregion 

}
