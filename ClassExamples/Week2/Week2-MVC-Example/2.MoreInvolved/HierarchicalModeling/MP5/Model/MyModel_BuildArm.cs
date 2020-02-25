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
        // Palm hierarchy support
        private float kArmLength = 5;
        private float kArmWidth = 7;

        private float kSphereScale = 1f / 13f;
        private float kCylinderLengthScale = 1f / 3f;
        private float kCylinderRadiusScale = 1f / 2f;

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
            c1.Xform.Translation = new Vector3(kArmLength/4f, 0f, 0);
            c1.Color = Color.DarkSlateBlue;
            node.AddShape(c1);
            c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(1.0f * kSphereScale);
            c1.Xform.Translation = new Vector3(kArmLength/2f, 0f, 0);
            c1.Color = Color.Black;
            node.AddShape(c1);
            c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(1.0f * kSphereScale);
            c1.Xform.Translation = new Vector3(kArmLength*0.75f, 0f, 0);
            c1.Color = Color.DarkSlateBlue;
            node.AddShape(c1);

            SceneNode palm = BuildPalmAt(kArmWidth/2f, "Top");
            node.AddChild(palm);

            palm = BuildPalmAt(0, "Mid");
            node.AddChild(palm);

            palm = BuildPalmAt(-kArmWidth/2f, "Bottom");
            node.AddChild(palm);

            return node;
        }

        private SceneNode BuildPalmAt(float atZ, String name)
        {
            float useXPos = kArmLength + 1f; // add the palm circle radius to it
            SceneNode palm = new SceneNode(name);
            palm.Xform.Pivot = new Vector3(kArmLength, 0, atZ);
            MeshShape c1 = new MeshShape("sphere");
            c1.Xform.Scale = new Vector3(kSphereScale);
            c1.Xform.Translation = new Vector3(useXPos, 0, atZ);
            c1.Color = Color.Blue;
            palm.AddShape(c1);
            MeshShape s1 = new MeshShape("teapot");
            s1.Xform.Scale = new Vector3(0.5f);
            s1.Xform.Translation = new Vector3(useXPos, 1.1f, atZ);
            s1.Color = Color.DeepPink;
            palm.AddShape(s1);
            s1 = new MeshShape("teapot");
            s1.Xform.Scale = new Vector3(0.4f);
            s1.Xform.Translation = new Vector3(useXPos + 1.1f, 0f, atZ);
            s1.Color = Color.DarkSeaGreen;
            palm.AddShape(s1);

            SceneNode finger = BuildFingerAt(kArmLength + 1, atZ + 1.0f, "f1");
            palm.AddChild(finger);

            finger = BuildFingerAt(kArmLength + 1, atZ - 1.0f, "f2");
            palm.AddChild(finger);

            return palm;
        }

        private SceneNode BuildFingerAt(float atX, float atZ, String name)
        {
            SceneNode aFinger = new SceneNode(name);

            // 
            const float fingerLength = 3f;
            float pivotXOffset = atX;
            aFinger.Xform.Pivot = new Vector3(pivotXOffset, 0, atZ);

            MeshShape finger = new MeshShape("cylinder");
            finger.Xform.RotateInYByRadian(MathHelper.PiOver2);
            finger.Xform.Scale = new Vector3(0.25f * kCylinderRadiusScale, 0.25f * kCylinderRadiusScale, fingerLength * kCylinderRadiusScale);
            finger.Xform.Translation = new Vector3(pivotXOffset + (fingerLength/2f), 0, atZ);
            finger.Color = Color.Purple;
            aFinger.AddShape(finger);
            MeshShape c1 = new MeshShape("teapot");
            c1.Xform.Scale = new Vector3(0.6f);
            c1.Xform.Translation = new Vector3(pivotXOffset + fingerLength, 0f, atZ);
            c1.Color = Color.DarkSlateGray;
            aFinger.AddShape(c1);

            return aFinger;
        }

    }
}