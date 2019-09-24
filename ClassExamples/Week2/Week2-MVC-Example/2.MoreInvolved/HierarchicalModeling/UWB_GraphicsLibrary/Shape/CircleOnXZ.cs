using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    public class CircleOnXZ : Shape
    {
        private const int kNumTriangles = 15;
        private const int kNumVertices = kNumTriangles + 1;
        private const int kNumIndices = kNumTriangles * 3;


        #region support on device vertex storage
        private class VertexData
        {
            public VertexBuffer mVertices { get; set; }
            public IndexBuffer mIndices { get; set; }
        };
        private Dictionary<GraphicsDevice, VertexData> mVerticesOnDevice;
        #endregion

        public CircleOnXZ()
            : base()
        {
            mVerticesOnDevice = new Dictionary<GraphicsDevice, VertexData>();
        }

        #region override from super class

        override protected void AllocateVertices()
        {
        }

        /// <summary>
        /// Computes vertices position based on mSize and mCenter.
        /// Assumes mSize>0
        /// </summary>
        override protected void ComputeVertexPosition()
        {
        }


        override protected void SetCurrentColor()
        {
        }
        #endregion

        private VertexData AllocateVertexData(GraphicsDevice graphicsDevice)
        {
            // 1. allocate the return vertexData
            VertexData vertexData = new VertexData();

            // notice we only really need kNumTriangle+1 number of vertices to repsent circle with kNumTriangles!
            // 8 triangle circle only needs 9 distinct vertices!
            vertexData.mVertices = new VertexBuffer(graphicsDevice, VertexPositionColor.VertexDeclaration, kNumVertices, BufferUsage.None);
            vertexData.mIndices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, kNumIndices, BufferUsage.None);

            // 2. temp CPU memory for storing transient data
            VertexPositionColor[] v = new VertexPositionColor[kNumVertices];
            Int16[] vertexIndices = new Int16[kNumIndices];

            float theta = (float)((Math.PI * 2) / kNumTriangles);

            v[0].Position = Vector3.Zero;  // is the center
            v[0].Color = Color.White;

            // Render as a list of triangles sharing one common vertex(center of the circumcircle)
            for (int i = 1; i <= kNumTriangles; i++)
            {
                // vertex position
                float x = (float)Math.Cos(theta * i);
                float y = 0;
                float z = (float)Math.Sin(theta * i);
                v[i].Position = new Vector3(x, y, z);
                v[i].Color = Color.Red;

                // index for one triangle
                int offset = (i-1) * 3;
                vertexIndices[offset] = 0; // All triangles shared the 0th index
                vertexIndices[offset + 1] = (Int16) i;
                vertexIndices[offset + 2] = (Int16) (i + 1); // the very last one will be wrong!
            }
            vertexIndices[kNumIndices - 1] = 1; //!!

            // set into the device memory
            vertexData.mVertices.SetData(v);
            vertexData.mIndices.SetData(vertexIndices);

            return vertexData;
        }

        protected override void DrawPrimitive(GraphicsDevice graphicsDevice, ContentManager cm)
        {
            VertexData d;
            if (mVerticesOnDevice.ContainsKey(graphicsDevice))
            {
                d = mVerticesOnDevice[graphicsDevice];
            }
            else
            {
                d = AllocateVertexData(graphicsDevice);
                mVerticesOnDevice.Add(graphicsDevice, d);
            }

            VertexPositionColor[] v = new VertexPositionColor[kNumVertices];
            d.mVertices.GetData<VertexPositionColor>(v);
            for (int i = 1; i<kNumVertices; i++)
                v[i].Color = mColor;
            d.mVertices.SetData(v);

            graphicsDevice.SetVertexBuffer(d.mVertices);
            graphicsDevice.Indices = d.mIndices;
            int numIndices = d.mIndices.IndexCount;
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, numIndices, 0, numIndices / 3);
        }

    }
}