using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeXform : MonoBehaviour {

    public enum ConcatenationMode
    {
        UseUnity = 0,
        UseOurOwn = 1
    };

    public Transform LocalTransform = null; // The one with black sphere
    public Transform WorldTransform = null; // The one with white sphere
    public ConcatenationMode Mode = ConcatenationMode.UseUnity;

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

        if (Mode == ConcatenationMode.UseUnity)
        {
            WorldTransform.localPosition = transform.position;
            WorldTransform.localRotation = transform.rotation;
            WorldTransform.localScale = transform.lossyScale;
        } else
        {
            Transform parentXform = transform.parent;
            Matrix4x4 parentTRS = Matrix4x4.TRS(parentXform.localPosition, parentXform.localRotation, parentXform.localScale);
            Matrix4x4 myTRS = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
            Matrix4x4 concatMatrix = parentTRS * myTRS;

            // now decomposite and get each components
            WorldTransform.localPosition = concatMatrix.GetColumn(3);
            Vector3 x = concatMatrix.GetColumn(0);
            Vector3 y = concatMatrix.GetColumn(1);
            Vector3 z = concatMatrix.GetColumn(2);
            Vector3 size = new Vector3(x.magnitude, y.magnitude, z.magnitude);
            WorldTransform.localScale = size;

            // Align rotation
            // WorldTransform.localRotation = Quaternion.LookRotation(z / size.z, y / size.y);
            // OR
            y.Normalize();
            z.Normalize();
            // First, align up
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, y)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, y);
            WorldTransform.localRotation = Quaternion.AngleAxis(angle, axis);
            // Now, align forward
            angle = Mathf.Acos(Vector3.Dot(WorldTransform.forward, z)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(WorldTransform.forward, z);
            Debug.Log("Rotation Axis: " + axis);
            WorldTransform.localRotation = Quaternion.AngleAxis(angle, axis) * WorldTransform.localRotation;
        }
	}
}
