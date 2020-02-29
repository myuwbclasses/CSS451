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
    public partial class MyModel
    {
        // Palm hierarchy support
        private MeshShape mCameraPos, mAtPos, mUpPos;
        private SceneNode mCameraRefNode, mLevel1, mLevel2;

        private Vector3 kInitPos = new Vector3(10f, 0, 1);
        private Vector3 kInitAt = new Vector3(11, 0, 1);
        private Vector3 kInitUp = new Vector3(10f, 1, 1);

        private void InitCameraParameters(SceneNode refNode)
        {
            mCameraPos = new MeshShape("sphere");
            mAtPos = new MeshShape("sphere");
            mUpPos = new MeshShape("sphere");
            mCameraPos.Xform.Scale = new Vector3(kSphereScale * 0.2f);
            mAtPos.Xform.Scale = new Vector3(kSphereScale * 0.2f);
            mUpPos.Xform.Scale = new Vector3(kSphereScale * 0.2f);
            mCameraRefNode = refNode;
            mLevel1 = mCameraRefNode.GetChildren()[1];
            mLevel2 = mLevel1.GetChildren()[0];

            mLevel1.Xform.RotateInZByRadian(-MathHelper.Pi/8.0f);
            mLevel2.Xform.RotateInZByRadian(-MathHelper.Pi/16.0f);
        }

        private void ComputeCameraPos()
        {
            Matrix trans;
            mTheStack.Push();
            {
                mRoot.Xform.ComputeWorldTransform(mTheStack);
                mCameraRefNode.Xform.ComputeWorldTransform(mTheStack);
                mLevel1.Xform.ComputeWorldTransform(mTheStack);
                mLevel2.Xform.ComputeWorldTransform(mTheStack);
                trans = mTheStack.GetTopOfStack();
            }
            mTheStack.Pop();
            
            mCameraPos.Xform.Translation = Vector3.Transform(kInitPos, trans);
            mAtPos.Xform.Translation = Vector3.Transform(kInitAt, trans);
            mUpPos.Xform.Translation = Vector3.Transform(kInitUp, trans);

            mCameraToControl.UpVector = mUpPos.Xform.Translation - mCameraPos.Xform.Translation;

            Vector3 v = mAtPos.Xform.Translation - mCameraPos.Xform.Translation;
            v.Normalize();

            float d = (mCameraToControl.LookAt - mCameraToControl.CameraPosition).Length();
            mCameraToControl.CameraPosition = mCameraPos.Xform.Translation;
            mCameraToControl.LookAt = mCameraToControl.CameraPosition + d * v;
        }
    }
}