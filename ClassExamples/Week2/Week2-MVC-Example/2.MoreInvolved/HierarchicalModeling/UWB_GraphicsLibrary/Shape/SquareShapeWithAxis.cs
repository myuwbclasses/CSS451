using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWB_GraphicsLibrary
{
    public class SquareShapeWithAxis : SquareOnXZ
    {
        AxisShape mMainAxis = new AxisShape();

        /// <summary>
        /// Override the super class Draw to draw the AxisShape
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public override void Draw(GraphicsDevice graphicsDevice, ContentManager cm, Camera camera, MatrixStack stack)
        {
            base.Draw(graphicsDevice, cm, camera, stack);

            mMainAxis.Xform.Translation = Xform.Translation;
            // mMainAxis.Scale = Scale;
            mMainAxis.Xform.Rotation = Xform.Rotation;
            mMainAxis.Draw(graphicsDevice, cm, camera, stack);
        }
    }
}