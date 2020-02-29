using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using UWB_GraphicsLibrary;

namespace ClassExample
{  
    public class CameraVisualization
    {
        // shapes representing the camera parameters
        SquareShapeWithAxis mLookAt;
        CircleOnXZ mCameraPosition;
        SquareOnXZ mUpVectorPosition;
        LineShape mViewVector;
        LineShape mUpVector;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CameraVisualization()
        {
            mLookAt = new SquareShapeWithAxis();
            mLookAt.Color = Color.White;
            mLookAt.DrawPivot = true;

            mCameraPosition = new CircleOnXZ();
            mCameraPosition.Color = Color.Black;
            mCameraPosition.Xform.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            mCameraPosition.DrawPivot = true;

            mUpVectorPosition = new SquareOnXZ();
            mUpVectorPosition.Xform.Scale = new Vector3(0.1f, 0.1f, 0.1f);
            mUpVectorPosition.Color = Color.Red;
            mUpVectorPosition.DrawPivot = true;

            mViewVector = new LineShape();
            mViewVector.Color = Color.White;

            mUpVector = new LineShape();
            mUpVector.Color = Color.Black;
        }

        /// <summary>
        /// Updates the state of visualization with the given camera's parameter
        /// </summary>
        /// <param name="aCamera"></param>
        public void UpdateWithCameraParameters(Camera aCamera)
        {
            mCameraPosition.Xform.Translation = aCamera.CameraPosition;
            mLookAt.Xform.Translation = aCamera.LookAt;

            // Note how we position the upVector Position: a displacement from CameraPosition!!
            mUpVectorPosition.Xform.Translation = aCamera.CameraPosition + (2f * aCamera.UpVector);

            mViewVector.SetEndPoints(aCamera.CameraPosition, aCamera.LookAt);
            mUpVector.SetEndPoints(aCamera.CameraPosition, mUpVectorPosition.Xform.Translation);
        }

        /// <summary>
        /// Draws the given camrea
        /// </summary>
        public void Draw(GraphicsDevice graphicsDevice, ContentManager cm, Camera camera, MatrixStack stack)
        {
            mCameraPosition.Draw(graphicsDevice, cm,camera, stack);
            mLookAt.Draw(graphicsDevice, cm, camera, stack);
            mUpVectorPosition.Draw(graphicsDevice, cm, camera, stack);
            mViewVector.Draw(graphicsDevice, cm, camera, stack);
            mUpVector.Draw(graphicsDevice, cm, camera, stack);
        } 
    }
}
