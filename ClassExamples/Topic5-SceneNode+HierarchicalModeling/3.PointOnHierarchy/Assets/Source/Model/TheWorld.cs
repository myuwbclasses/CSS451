using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TheWorld : MonoBehaviour  {

    public List<SceneNode> TheRoots;

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (SceneNode root in TheRoots) {
        Matrix4x4 i = Matrix4x4.identity;
            root.CompositeXform(ref i);
        }
    }
}
