using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    /// <summary>
    /// </summary>
    public class LineShape : Shape
    {
        override protected void AllocateVertices()
        {
            mVertices = new VertexPositionColor[2];
        }

        /// <summary>
        /// </summary>
        override protected void ComputeVertexPosition() {

            float size = 5f;

            mVertices[0].Position = Vector3.UnitX * -size;
            mVertices[0].Color = Color.Black;

            mVertices[1].Position = Vector3.UnitX * size;
            mVertices[1].Color = Color.Black;
        }

        /// <summary>
        /// Sets the end points of the line
        /// </summary>
        /// <param name="pt1">First Pt</param>
        /// <param name="pt2">Second Pt</param>
        public void SetEndPoints(Vector3 pt1, Vector3 pt2)
        {
            mVertices[0].Position = pt1;
            mVertices[1].Position = pt2;
        }

        protected override void DrawPrimitive(GraphicsDevice graphicsDevice, ContentManager cm)
        {
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mVertices, 0, 1);
        }

    }
}