using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    public class AxisShape : Shape
    {
        override protected void AllocateVertices()
        {
            mVertices = new VertexPositionColor[6];
        }

        /// <summary>
        /// Computes vertices position based on mSize and mCenter.
        /// Assumes mSize>0
        /// </summary>
        override protected void ComputeVertexPosition()
        {
            float size = 2f;
            // X-axis
            mVertices[0] = new VertexPositionColor(new Vector3(-size, 0, 0), Color.Red);
            mVertices[1] = new VertexPositionColor(new Vector3(size, 0, 0), Color.Red);

            mVertices[2] = new VertexPositionColor(new Vector3(0, -size, 0), Color.Green);
            mVertices[3] = new VertexPositionColor(new Vector3(0, size, 0), Color.Green);

            mVertices[4] = new VertexPositionColor(new Vector3(0, 0, -size), Color.Blue);
            mVertices[5] = new VertexPositionColor(new Vector3(0, 0, size), Color.Blue);
        }

        protected override void SetCurrentColor()
        {
            // nothing to do
        }

        /// override the super class Draw to draw lines instead of triangles.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        override protected void DrawPrimitive(GraphicsDevice graphicsDevice, ContentManager cm)
        {
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mVertices, 0, 3);
        }
    }
}


   
        