using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    GameObject[] mControllers;

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i =0; i<v.Length; i++ )
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;
        }
    }
}
