using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePlacement : MonoBehaviour {

    public Vector2 Offset = Vector2.zero;
    public Vector2 Scale = Vector2.one;
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.SetFloat("MyTexOffset_X", Offset.x);
        GetComponent<Renderer>().material.SetFloat("MyTexOffset_Y", Offset.y);

        GetComponent<Renderer>().material.SetFloat("MyTexScale_X", Scale.x);
        GetComponent<Renderer>().material.SetFloat("MyTexScale_Y", Scale.y);
    }
}
