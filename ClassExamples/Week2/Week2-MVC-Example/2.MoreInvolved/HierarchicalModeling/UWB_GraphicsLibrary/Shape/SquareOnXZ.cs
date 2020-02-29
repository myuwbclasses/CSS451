using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UWB_GraphicsLibrary
{
    public class SquareOnXZ : Shape
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
            float offset = 0.5f;

            // First triangle
            mVertices[0].Position = (Vector3.UnitX * -offset) + (Vector3.UnitZ *  offset);
            mVertices[1].Position = (Vector3.UnitX *  offset) + (Vector3.UnitZ *  offset);
            mVertices[2].Position = (Vector3.UnitX *  offset) + (Vector3.UnitZ * -offset);

            // Second one
            mVertices[3].Position = (Vector3.UnitX * -offset) + (Vector3.UnitZ *  offset);
            mVertices[4].Position = (Vector3.UnitX *  offset) + (Vector3.UnitZ * -offset);
            mVertices[5].Position = (Vector3.UnitX * -offset) + (Vector3.UnitZ * -offset);

        }
    }
}