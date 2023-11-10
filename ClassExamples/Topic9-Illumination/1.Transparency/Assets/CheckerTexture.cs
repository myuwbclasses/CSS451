using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerTexture : MonoBehaviour {

    public float URepeat = 2;
    public Color Color1 = Color.black;

    public float VRepeat = 2;
    public Color Color2 = Color.white;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (URepeat < 2)
            URepeat = 2;
        if (VRepeat < 2)
            VRepeat = 2;

        GetComponent<Renderer>().material.SetFloat("URepeat", URepeat);
        GetComponent<Renderer>().material.SetFloat("VRepeat", VRepeat);
        GetComponent<Renderer>().material.SetColor("Color1", Color1);
        GetComponent<Renderer>().material.SetColor("Color2", Color2);
    }
}

