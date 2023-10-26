using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeXform : MonoBehaviour {
    public Transform LocalTransform = null; // The one with black sphere
    public Transform WorldTransform = null; // The one with white sphere
	// Use this for initialization
	void Start () {
        Debug.Assert(LocalTransform != null);
        Debug.Assert(WorldTransform != null);
    }
	
	// Update is called once per frame
	void Update () {
        LocalTransform.localPosition = transform.localPosition;
        LocalTransform.localRotation = transform.localRotation;
        LocalTransform.localScale = transform.localScale;

        WorldTransform.localPosition = transform.position;
        WorldTransform.localRotation = transform.rotation;
        WorldTransform.localScale = transform.lossyScale;
	}
}
