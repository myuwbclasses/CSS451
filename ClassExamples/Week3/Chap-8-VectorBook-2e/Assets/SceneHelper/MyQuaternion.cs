using UnityEngine;

public class MyQuaternion
{
    private MyVector mAxis;
    private MyXZPlane mPlaneOfRotation;
    private MyLineSegment mToInit, mToRotated;
    private const float kLineWidth = 0.08f;
    public Color InitColor { 
        set {
            mToInit.VectorColor = value;
        }
    }
    public Color RotatedColor {
         set {
            mAxis.VectorColor = value;
            mToRotated.VectorColor = value;
            mPlaneOfRotation.PlaneColor = Color.white;
        }
    }


    public MyQuaternion()
    {
        mPlaneOfRotation = new MyXZPlane
        {
            XSize = 2.5f,
            YSize = 2.5f,
            ZSize = 2.5f
        };
        mAxis = new MyVector();
        mToInit = new MyLineSegment {
            LineWidth = kLineWidth
        };
        mToRotated = new MyLineSegment {
            LineWidth = kLineWidth
        };
    }

    public void ShowRotation(Vector3 axisPt, Vector3 initPos, Vector3 rotatedPos) {
        mAxis.VectorFromTo(-axisPt, axisPt);
        Vector3 na = axisPt.normalized;

        Vector3 PiOnAxis = axisPt + Vector3.Dot((initPos-axisPt), na) * na;
        float s = (initPos - PiOnAxis).magnitude; 
        mPlaneOfRotation.PlaneNormal = na;
        mPlaneOfRotation.Center = PiOnAxis;
        mPlaneOfRotation.XSize = mPlaneOfRotation.ZSize = 1.2f * s;

        mToInit.VectorFromTo(PiOnAxis, initPos);
        mToRotated.VectorFromTo(PiOnAxis, rotatedPos);
    }

    public bool ShowQuaternion {
        set {
            mPlaneOfRotation.DrawPlane = value;
            mToInit.DrawVector = value;
            mToRotated.DrawVector = value;
            mAxis.DrawVector = value;
        }
    }
};