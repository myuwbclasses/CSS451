using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    public class MeshShape : Shape
    {
        String mMeshName;
        private Dictionary<GraphicsDevice, Model> mMeshOnDevice;

        public MeshShape(String meshName)
        {
            mMeshOnDevice = new Dictionary<GraphicsDevice, Model>();
            mMeshName = meshName;
        }

        #region super class stuff
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

        protected override void SetCurrentColor()
        {
            // nothing to do
        }
        #endregion

        /// override the super class Draw to draw lines instead of triangles.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        override protected void DrawPrimitive(GraphicsDevice graphicsDevice, ContentManager cm)
        {
            Model m;

            if (mMeshOnDevice.ContainsKey(graphicsDevice))
            {
                m = mMeshOnDevice[graphicsDevice];
            }
            else
            {
                m = cm.Load<Model>(mMeshName);
                mMeshOnDevice.Add(graphicsDevice, m);
            }


            BasicEffect effect = LoadEffect(graphicsDevice);
            effect.VertexColorEnabled = false;
            effect.LightingEnabled = true;
            
            // Render the tiger
            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    BasicEffect e = part.Effect as BasicEffect;
                    if (null != e)
                    {
                        e.World = effect.World;
                        e.View = effect.View;
                        e.Projection = effect.Projection;
                        e.EnableDefaultLighting();
                        e.EmissiveColor = mColor.ToVector3();
                    }
                    else
                    {
                        part.Effect = effect;
                        effect.EmissiveColor = mColor.ToVector3();
                    }
                }
                mesh.Draw();
            }
        }
    }
}


   
        