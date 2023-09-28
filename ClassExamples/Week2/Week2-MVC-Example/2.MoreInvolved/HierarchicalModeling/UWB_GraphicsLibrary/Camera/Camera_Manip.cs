#define SideRot_Check

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UWB_GraphicsLibrary
{
    /// <summary>
    /// Supports manipulation of camera ...
    /// </summary>
    public partial class Camera
    {
        const float kClosestViewDist = 0.5f;

        /// <summary>
        /// Rotate the camera about the Up vector ...
        /// </summary>
        /// <param name="radian"></param>
        public void RotateCameraAboutUp(float radian)
        {
            Matrix m = Matrix.CreateRotationY(radian);

            // in case when the lookAt position is not the origin
            Matrix invT = Matrix.CreateTranslation(-mLookAt);
            Matrix t = Matrix.CreateTranslation(mLookAt);

            // WHAT ARE WE DOING HERE?
            m = invT * m * t;
            mCameraPosition = Vector3.Transform(mCameraPosition, m);
        }

        public void RotateCameraAboutSide(float radian)
        {
            Vector3 viewDirection = mLookAt - mCameraPosition;
            
            #region Checking to avoid the problem ... not really that elegant, but hey, it works
#if SideRot_Check
            const float kAlmostOne = 0.95f;
            viewDirection.Normalize();
            if (Math.Abs(Vector3.Dot(viewDirection, mUpVector)) > kAlmostOne)
            {
                if (((mCameraPosition.Y > 0) && (radian > 0f)) ||
                     ((mCameraPosition.Y < 0) && (radian < 0f)))
                    return;
            }
#endif
            #endregion 

            Vector3 side = Vector3.Cross(mUpVector, viewDirection);
            side.Normalize(); // NOTE: must normalize, for CreateFromAxis expects this to be a noramlized vector!

            // Simplicity rotation: observe that without the explicit checking and avoiding the problem
            // the camera gets stuck when viewDirection and Up vector aligns!
            Matrix m = Matrix.CreateFromAxisAngle(side, radian);

            // in case when the lookAt position is not the origin
            Matrix invT = Matrix.CreateTranslation(-mLookAt);
            Matrix t = Matrix.CreateTranslation(mLookAt);

            // WHAT ARE WE DOING HERE?
            m = invT * m * t;
            mCameraPosition = Vector3.Transform(mCameraPosition, m);
        }

        
        public void ZoomCamera(float delta)
        {
            Vector3 viewDirection = mLookAt - mCameraPosition;
            float dist = viewDirection.Length(); 
                // current distance between CameraPosition and lookat
            viewDirection /= dist;
            dist -= delta;
            if (dist < kClosestViewDist)
                dist = kClosestViewDist;
            mCameraPosition = mLookAt - dist * viewDirection;
        }

        public void PanCamera(float dx, float dy)
        {
            Vector3 v = mLookAt - mCameraPosition;
            Vector3 side = Vector3.Cross(mUpVector, v);
            Vector3 u = Vector3.Cross(v, side);
            side.Normalize();
            u.Normalize();

            Vector3 delta = dx * side + dy * u;
            mCameraPosition += delta;
            mLookAt += delta;
        }
    }
}
