using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UWB_GraphicsLibrary
{
    public partial class Camera
    {
        private Vector3 mCameraPosition, mLookAt, mUpVector;
        private float mNearPlane, mFarPlane, mFOV, mAspectRatio;

        #region data member accessors
        public Vector3 CameraPosition { get { return mCameraPosition; } set { mCameraPosition = value; } }
        public Vector3 LookAt { get { return mLookAt; } set { mLookAt = value; } }
        public Vector3 UpVector { get { return mUpVector; } set { mUpVector = value; } }
        public float NearPlane { get { return mNearPlane; } set { mNearPlane = value; } }
        public float FarPlane { get { return mFarPlane; } set { mFarPlane = value; } }
        public float FOV { get { return mFOV; } set { mFOV = value; } }
        public float AspectRatio { get { return mAspectRatio; } set { mAspectRatio = value; } }
        #endregion 

        private void DefaultViewVolumn()
        {
            NearPlane = 1f;
            FarPlane = 1000f;
            FOV = MathHelper.Pi / 4f;
        }

        /// <summary>
        /// Default camera
        /// </summary>
        public Camera()
        {
            CameraPosition = new Vector3(0, 10, 10);
            LookAt = Vector3.Zero;
            UpVector = Vector3.UnitY;
            AspectRatio = 1f;
            DefaultViewVolumn();
        }
        /// <summary>
        /// Creates a camera with defualt view volumn n=1, f=1000, fov=45-degrees
        /// </summary>
        /// <param name="cameraPos">The camera position</param>
        /// <param name="at">Look at position</param>
        /// <param name="up">Up vector</param>
        /// <param name="aspectRatio">aspect ratio of the draw area</param>
        public Camera(Vector3 cameraPos, Vector3 at, Vector3 up, float aspectRatio)
        {
            CameraPosition = cameraPos;
            LookAt = at;
            UpVector = up;
            AspectRatio = aspectRatio;
            DefaultViewVolumn();
        }

        /// <summary>
        /// Creates a camera with complete parameter spec
        /// </summary>
        /// <param name="cameraPos">camera position</param>
        /// <param name="at">look at position</param>
        /// <param name="up">up vector</param>
        /// <param name="n">distance to near plane</param>
        /// <param name="f">distance to far plane</param>
        /// <param name="fov">fields of view (in radians)</param>
        /// <param name="aspectRatio">aspect ratio on the draw area</param>
        public Camera(Vector3 cameraPos, Vector3 at, Vector3 up, 
                        float n, float f, float fov, float aspectRatio)
        {
            CameraPosition = cameraPos;
            LookAt = at;
            UpVector = up;
            AspectRatio = aspectRatio;
            NearPlane = n;
            FarPlane = f;
            FOV = fov;
        }

        public Matrix ViewMatrix()
        {
            return Matrix.CreateLookAt(CameraPosition, LookAt, UpVector);
            
        }

        public Matrix ProjectionMatrix()
        {
            return Matrix.CreatePerspectiveFieldOfView(FOV, AspectRatio, NearPlane, FarPlane);
        }
    }
}
