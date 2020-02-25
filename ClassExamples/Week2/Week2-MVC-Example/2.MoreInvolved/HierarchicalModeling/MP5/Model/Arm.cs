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
    public class Arm : SceneNode
    {
        
        // Palm hierarchy support
        internal const float kArmLength = 5;
        internal const float kArmWidth = 7;

        internal const float kSphereScale = 1f / 13f;
        internal const float kCylinderLengthScale = 1f / 3f;
        internal const float kCylinderRadiusScale = 1f / 2f;

        private SceneNode BuildArm(String name)
        {
            SceneNode node = new SceneNode(name);
            MeshShape m = new MeshShape("cylinder");
            m.Color = Color.Blue;
            node.AddShape(m);


            // arm and the decorations
            MeshShape arm = new MeshShape("cylinder");
            arm.Xform.RotateInYByRadian(MathHelper.PiOver2);
            arm.Xform.Scale = new Vector3(kArmWidth * kCylinderRadiusScale, 0.7f, kArmLength * kCylinderLengthScale);
            arm.Xform.Translation = new Vector3(kArmLength / 2f, 0, 0);
            node.AddShape(arm);
            MeshShape c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(1.0f * kSphereScale);
            c1.Xform.Translation = new Vector3(kArmLength / 4f, 0f, 0);
            c1.Color = Color.DarkSlateBlue;
            node.AddShape(c1);
            c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(1.0f * kSphereScale);
            c1.Xform.Translation = new Vector3(kArmLength / 2f, 0f, 0);
            c1.Color = Color.Black;
            node.AddShape(c1);
            c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(1.0f * kSphereScale);
            c1.Xform.Translation = new Vector3(kArmLength * 0.75f, 0f, 0);
            c1.Color = Color.DarkSlateBlue;
            node.AddShape(c1);

            node.AddChild(new Palm(kArmWidth / 2f, "Top"));
            node.AddChild(new Palm(0, "Mid"));
            node.AddChild(new Palm(-kArmWidth / 2f, "Bottom"));

            return node;
        }
    }

    public class Palm : SceneNode
    {
        public Palm (float atZ, String name) 
                : base(name)
        {
            float useXPos = Arm.kArmLength + 1f; // add the palm circle radius to it
            Xform.Pivot = new Vector3(Arm.kArmLength, 0, atZ);
            MeshShape c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(Arm.kSphereScale);
            c1.Xform.Translation = new Vector3(useXPos, 0, atZ);
            c1.Color = Color.Blue;
            AddShape(c1);
            MeshShape s1 = new MeshShape("teapot");
            s1.Xform.Scale = new Vector3(0.5f);
            s1.Xform.Translation = new Vector3(useXPos, 1.1f, atZ);
            s1.Color = Color.DeepPink;
            AddShape(s1);
            s1 = new MeshShape("teapot");
            s1.Xform.Scale = new Vector3(0.4f);
            s1.Xform.Translation = new Vector3(useXPos + 1.1f, 0f, atZ);
            s1.Color = Color.DarkSeaGreen;
            AddShape(s1);

            AddChild(new Finger(Arm.kArmLength + 1, atZ + 1.0f, "f1"));
            AddChild(new Finger(Arm.kArmLength + 1, atZ - 1.0f, "f2"));
        }
    }

    public class Finger : SceneNode
    {

        public Finger(float atX, float atZ, String name) : base (name)
        {
            const float fingerLength = 3f;
            float pivotXOffset = atX;
            Xform.Pivot = new Vector3(pivotXOffset, 0, atZ);

            MeshShape finger = new MeshShape("cylinder");
            Xform.RotateInYByRadian(MathHelper.PiOver2);
            Xform.Scale = new Vector3(0.25f * Arm.kCylinderRadiusScale, 0.25f * Arm.kCylinderRadiusScale, fingerLength * Arm.kCylinderRadiusScale);
            finger.Xform.Translation = new Vector3(pivotXOffset + (fingerLength / 2f), 0, atZ);
            finger.Color = Color.Purple;
            AddShape(finger);
            MeshShape c1 = new MeshShape("teapot");
            c1.Xform.Scale = new Vector3(0.6f);
            c1.Xform.Translation = new Vector3(pivotXOffset + fingerLength, 0f, atZ);
            c1.Color = Color.DarkSlateGray;
            AddShape(c1);
        }     
    }
}