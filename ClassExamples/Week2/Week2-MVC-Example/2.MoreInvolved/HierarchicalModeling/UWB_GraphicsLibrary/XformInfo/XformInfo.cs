using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UWB_GraphicsLibrary
{
    public class XformInfo
    {
        Vector3 mTranslation;
        Vector3 mScale;
        Quaternion mRotation;
        Vector3 mPivot;

        public XformInfo()
        {
            ToIdentity();
        }

        public Matrix ComputeWorldTransform(MatrixStack stack)
        {
            stack.Translation(mPivot);
            stack.Translation(mTranslation);
            stack.Rotate(mRotation);
            stack.Scale(mScale);
            stack.Translation(-mPivot);

            return stack.GetTopOfStack();
        }

        public void ToIdentity()
        {
            mTranslation = Vector3.Zero;
            mScale = Vector3.One;
            mRotation = Quaternion.Identity;
            mPivot = Vector3.Zero;
        }

        // accessor
        public Vector3 Translation { get { return mTranslation; } set { mTranslation = value; } }
        public Vector3 Scale { get { return mScale; } set { mScale = value; } }
        public Quaternion Rotation { get { return mRotation; } set { mRotation = value; } }
        public Vector3 Pivot { get { return mPivot; } set { mPivot = value; } }

        #region Rotation support
        public void RotateInXByRadian(float rad)
        {
            Rotation = mRotation * Quaternion.CreateFromAxisAngle(Vector3.UnitX, rad);
        }
        public void RotateInYByRadian(float rad)
        {
            Rotation = mRotation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, rad);
        }
        public void RotateInZByRadian(float rad)
        {
            Rotation = mRotation * Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rad);
        }
        #endregion

    }
}
