#region File Description
//-----------------------------------------------------------------------------
// GraphicsDeviceService.cs
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
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
#endregion

// The IGraphicsDeviceService interface requires a DeviceCreated event, but we
// always just create the device inside our constructor, so we have no place to
// raise that event. The C# compiler warns us that the event is never used, but
// we don't care so we just disable this warning.
#pragma warning disable 67

namespace XNAWinFormLibrary
{   
    /// <summary>
    /// Helper class responsible for creating and managing the GraphicsDevice.
    /// All GraphicsDeviceControl instances share the same GraphicsDeviceService,
    /// so even though there can be many controls, there will only ever be a single
    /// underlying GraphicsDevice. This implements the standard IGraphicsDeviceService
    /// interface, which provides notification events for when the device is reset
    /// or disposed.
    /// </summary>
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        /// <summary>
        /// This is the abstract connection to the graphics hardware
        /// </summary>
        GraphicsDevice mGraphicsDevice;

        /// <summary>
        /// Store the current device settings.
        /// </summary>
        PresentationParameters mParameter;

        #region for IGraphicsDeviceService
        // IGraphicsDeviceService events.
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        #endregion

        /// <summary>
        /// This is like the rendering pipeline
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return mGraphicsDevice; }
        }

        /// <summary>
        /// Constructor is private, because this is a singleton class:
        /// client controls should use the public AddRef method instead.
        /// </summary>
        /// <param name="windowHandle">This is the GUI drawing area</param>
        /// <param name="width">width of drawing area</param>
        /// <param name="height">height of drawing area</param>
        public GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            mParameter = new PresentationParameters();

            mParameter.BackBufferWidth = Math.Max(width, 1);
            mParameter.BackBufferHeight = Math.Max(height, 1);
            mParameter.BackBufferFormat = SurfaceFormat.Color;
            mParameter.DepthStencilFormat = DepthFormat.Depth24;
            mParameter.DeviceWindowHandle = windowHandle;
            mParameter.PresentationInterval = PresentInterval.Immediate;
            mParameter.IsFullScreen = false;

            mGraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,     // connect to the default adapter
                                                GraphicsProfile.Reach,              // using this graphics capability
                                                mParameter);                        // this is the display parameter
        }

        /// <summary>
        /// Releases a reference to the singleton instance.
        /// </summary>
        public void Release(bool disposing)
        {
            {
                // If this is the last control to finish using the
                // device, we should dispose the singleton instance.
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    mGraphicsDevice.Dispose();
                }

                mGraphicsDevice = null;
            }
        }

        
        /// <summary>
        /// Resets the graphics device to whichever is bigger out of the specified
        /// resolution or its current size. This behavior means the device will
        /// demand-grow to the largest of all its GraphicsDeviceControl clients.
        /// </summary>
        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            mParameter.BackBufferWidth = Math.Max(mParameter.BackBufferWidth, width);
            mParameter.BackBufferHeight = Math.Max(mParameter.BackBufferHeight, height);

            mGraphicsDevice.Reset(mParameter);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }

    }
}
