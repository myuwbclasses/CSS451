using UnityEngine;
public class MyAxisFrame {
    private MyVector X, Y, Z;
    
    public float Size {
        set { 
            X.Magnitude = value;
            Y.Magnitude = value;
            Z.Magnitude = value;
         }
        get { return X.Magnitude; }
    }

    public Vector3 At {
        set {
            X.VectorAt = value;
            Y.VectorAt = value;
            Z.VectorAt = value;
        } 
    }

    // Assume x/y/zDir are proper: orthonormal
    public void SetFrame(Vector3 xDir, Vector3 yDir, Vector3 zDir) {
        X.Direction = xDir;
        Y.Direction = yDir;
        Z.Direction = zDir;
    }

    public MyAxisFrame() {
        X = new MyVector {
            VectorColor = Color.red,
            Magnitude = 2f,
            VectorAt = Vector3.zero
        };
        Y = new MyVector {
            VectorColor = Color.green,
            Magnitude = 2f,
            VectorAt = Vector3.zero
        };
        Z = new MyVector {
            VectorColor = Color.blue,
            Magnitude = 2f,
            VectorAt = Vector3.zero
        };
    }
}