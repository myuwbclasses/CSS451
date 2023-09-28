using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    abstract public class Shape
    {
        // For storing effects!
        Dictionary<GraphicsDevice, BasicEffect> mEffectOnDevice;

        // Subclass should allocate this!!
        protected VertexPositionColor[] mVertices;

        private Vector3 mCenter;
        protected Color mColor;

        private XformInfo mXform;

        private bool mDrawPivot;
        AxisShape mPivot;

        public Shape()
        {
            mCenter = Vector3.Zero;
            mColor = Color.Red;

            mXform = new XformInfo();
            mPivot = null; // only allocate if wants to draw, instantiate now creates a infinite construction
            mDrawPivot = false;

            AllocateVertices();
            ComputeVertexPosition();

            mEffectOnDevice = new Dictionary<GraphicsDevice, BasicEffect>();
        }

        // accessors ...
        public Color Color { get { return mColor; } set { mColor = value; } }
        public XformInfo Xform { get { return mXform; } set { mXform = value; } }
        public bool DrawPivot { get { return mDrawPivot; } set { mDrawPivot = value; } }

               
        #region for subclass to override on the details of the Shape geometry
        /// <summary>
        /// Subclass must override this and allocate the contents of the mVertices
        /// </summary>
        abstract protected void AllocateVertices();

        /// <summary>
        /// Computes vertices position based on mSize and mCenter.
        /// Assumes mSize>0
        /// </summary>
        abstract protected void ComputeVertexPosition();

        /// <summary>
        /// Sets the current color into the vertex data structure
        /// </summary>
        virtual protected void SetCurrentColor()
        {
            for (int i = 0; i < mVertices.Length; i++)
                mVertices[i].Color = Color;
        }

        /// <summary>
        /// Draws the primitive using the vertices
        /// </summary>
        virtual protected void DrawPrimitive(GraphicsDevice graphicsDevice, ContentManager cm)
        {
            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, mVertices, 0, mVertices.Length / 3);
        }

        #endregion

        #region Loading of Effect
        virtual protected BasicEffect LoadEffect(GraphicsDevice graphicsDevice)
        {
            BasicEffect e;
            if (mEffectOnDevice.ContainsKey(graphicsDevice))
            {
                e = mEffectOnDevice[graphicsDevice];
            }
            else
            {
                e = new BasicEffect(graphicsDevice);
                mEffectOnDevice.Add(graphicsDevice, e);
                e.VertexColorEnabled = true;
            }
            return e;
        }
        #endregion
        
        /// <summary>
        /// if sub-class really wants to, can override this and determines how to draw
        /// </summary>
        /// <param name="graphicsDevice"></param>
        virtual public void Draw(GraphicsDevice graphicsDevice, ContentManager cm, Camera camera, MatrixStack stack)
        {
            BasicEffect effect = LoadEffect(graphicsDevice);

            stack.Push();
            {
                Xform.ComputeWorldTransform(stack);

                effect.View = camera.ViewMatrix();
                effect.Projection = camera.ProjectionMatrix();
                effect.World = stack.GetTopOfStack();
                effect.CurrentTechnique.Passes[0].Apply();

                SetCurrentColor();
                DrawPrimitive(graphicsDevice, cm);
            }
            stack.Pop();

            if (mDrawPivot)
            {
                if (null == mPivot)
                    mPivot = new AxisShape();

                mPivot.DrawPivot = false; // to ensure no infinite recursion!
                mPivot.Xform.Translation = Xform.Translation + Xform.Pivot;
                mPivot.Xform.Rotation = Xform.Rotation;
                mPivot.Draw(graphicsDevice, cm, camera, stack);
            }
        }
    }
}
