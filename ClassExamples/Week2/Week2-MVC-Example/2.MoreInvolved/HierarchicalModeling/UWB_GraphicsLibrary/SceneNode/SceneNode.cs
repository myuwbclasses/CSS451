/// Ms - SceneNode Transform
/// Ms.P sceneNode Pivot
/// Ms.T/S/R: SceneNode Translate/Scale/Rotate
///
// #define M1
/// This method draw itself by seeing:
///      Ms.P * Ms
/// Notice, Ms is: Translate(-Ms.P) * Scale(Ms.S) * Rotate(Ms.R) * Translate(Ms.T) * Translate(Ms.P)
/// We are essentially doing:
/// 
///         Translate(Ms.P) * Translate(-Ms.P) * Scale(Ms.S) * Rotate(Ms.R) * Translate(Ms.T) * Translate(Ms.P)
/// Or:
///                                              Scale(Ms.S) * Rotate(Ms.R) * Translate(Ms.T) * Translate(Ms.P)

#define M2
/// We poped off Ms from the stack and copied: Ms.P, Ms.T, Ms.R
/// So we are implementing:
/// 
///                                              Rotate(Ms.R) * Translate(Ms.T) * Translate(Ms.P)
/// Without the scaling factor!

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UWB_GraphicsLibrary
{
	public class SceneNode
	{
        String mName;
        List<Shape> mShapes;
        List<SceneNode> mChildren;

        XformInfo mXform;
        bool mShowPivot;
        AxisShape mPivotShape;

		public SceneNode(String name = "scene node")
		{
            mName = name;
            mXform = new XformInfo();
            mShapes = new List<Shape>();
            mChildren = new List<SceneNode>();

            mShowPivot = false;
            mPivotShape = new AxisShape();
            mPivotShape.Xform.Scale = new Vector3(5f);
		}

        public void Draw(GraphicsDevice device, ContentManager cm, Camera camera, MatrixStack stack)
        {
            stack.Push();
            {
                mXform.ComputeWorldTransform(stack);

                foreach (var s in mShapes)
                    s.Draw(device, cm, camera, stack);

                foreach (var c in mChildren)
                    c.Draw(device, cm, camera, stack);

#if M1
                if (mShowPivot)
                {
                    mPivotShape.Xform.Translation = Xform.Pivot;
                    mPivotShape.Draw(device, cm, camera, stack);
                }
#endif 
            }
            stack.Pop();

#if M2
            if (mShowPivot)
                {
                    mPivotShape.Xform.Translation = Xform.Pivot + Xform.Translation;
                    mPivotShape.Xform.Rotation = Xform.Rotation;

                    mPivotShape.Draw(device, cm, camera, stack);
                }
#endif

        }

        public void AddShape(Shape s)
        {
            mShapes.Add(s);
        }

        public void AddChild(SceneNode s)
        {
            mChildren.Add(s);
        }

        public List<SceneNode> GetChildren()
        {
            return mChildren;
        }

        public Shape GetShapeAtIndex(int i)
        {
            Shape s = null;
            if (i < mShapes.Count)
                s = mShapes[i];
            return s;
        }

        public int GetNumShapesInNode() { return mShapes.Count; }

        public XformInfo Xform { get { return mXform; } }
        public String Name { get { return mName; } }
        public bool ShowPivot { get { return mShowPivot; } set { mShowPivot = value; } }
	}
}
