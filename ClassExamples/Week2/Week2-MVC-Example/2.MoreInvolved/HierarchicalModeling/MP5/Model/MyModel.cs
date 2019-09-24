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
        AxisShape mMajorAxis;
        MatrixStack mTheStack;

        SceneNode mRoot;

        Camera mCameraToControl;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyModel() 
        {
            mTheStack = new MatrixStack();

            mMajorAxis = new AxisShape();
            mMajorAxis.Xform.Scale = new Vector3(10, 10, 10);

            mRoot = new SceneNode("Root");

            SceneNode arm = BuildArm("TopArm");
            mRoot.AddChild(arm);
            arm.Xform.RotateInYByRadian(MathHelper.PiOver2);
            arm.Xform.Translation = new Vector3(0, 15, 20);
            
            InitCameraParameters(arm);

            arm = BuildArm("BottomArm");
            mRoot.AddChild(arm);
            arm.Xform.Translation = new Vector3(0, 2, 0);

            SceneNode others = new SceneNode("Other Stuff");
            mRoot.AddChild(others);
            
            MeshShape m = new MeshShape("cone");
            m.Xform.Translation = new Vector3(-10, 3, 0);
            others.AddShape(m);

            SquareOnXZ s = new SquareOnXZ();
            s.Xform.Scale = new Vector3(20, 20, 20);
            s.Xform.Translation = new Vector3(0, -2, 0);
            s.Color = Color.DarkGray;
            others.AddShape(s);

            m = new MeshShape("shusui");
            m.Xform.Translation = new Vector3(8, 2, -10);
            m.Xform.Scale = new Vector3(4f);
            others.AddShape(m);

            m = new MeshShape("bigship1");
            m.Xform.Translation = new Vector3(-5, 2, -5);
            m.Xform.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            others.AddShape(m);
        }

        /// <summary>
        /// Draws the entire model: in this case trivial. Major axis and the square.
        /// </summary>
        /// <param name="graphicsDevice">this is the display area we need to draw to.</param>
        public void Draw(GraphicsDevice graphicsDevice, ContentManager contentManager, Camera viewCamera)
        {
            mMajorAxis.Draw(graphicsDevice, contentManager, viewCamera, mTheStack);

            mRoot.Draw(graphicsDevice, contentManager, viewCamera, mTheStack);

        }

        public void DrawCameraPos(GraphicsDevice graphicsDevice, ContentManager contentManager, Camera viewCamera)
        {
                mUpPos.Draw(graphicsDevice, contentManager, viewCamera, mTheStack);
                mAtPos.Draw(graphicsDevice, contentManager, viewCamera, mTheStack);
                mCameraPos.Draw(graphicsDevice, contentManager, viewCamera, mTheStack);
        }

        public void UpdateModel()
        {
            ComputeCameraPos();
        }

        public SceneNode GetRootNode()
        {
            return mRoot;
        }

        public void SetCameraToControl(Camera c) { mCameraToControl = c; }
        
    }
}