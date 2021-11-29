using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject Init, Final;  // defines the initial and final arrows
    public bool LastUseUnity = false;
    public bool ShowSteps = true;
    private GameObject[] steps; 
    private int kNumSteps = 15;

private void Start()
    {
        Init.GetComponent<Renderer>().material.color = Color.black;
        steps = new GameObject[kNumSteps];
        for (int i = 0; i< 15; i++ )
        {
            steps[i] = Instantiate(Resources.Load("Arrow")) as GameObject;
            steps[i].transform.localScale = Init.transform.localScale;
            steps[i].transform.localPosition = Init.transform.localPosition;
            steps[i].transform.localRotation = Init.transform.localRotation;
        }

        steps[kNumSteps - 1].GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Update()
    {
        Vector3 initUp = Init.transform.up; // Vector3.up; //  
        Vector3 finalUp = Final.transform.up;
        Vector3 v1v2Axis = Vector3.Cross(initUp, finalUp);
        float v1v2Theta = Mathf.Acos(Vector3.Dot(initUp, finalUp)) * Mathf.Rad2Deg; // we know v1 and v2 are normalized

       // if (v1v2Axis.magnitude < float.Epsilon)
         //   return;

        if (ShowSteps)
        {
            #region // the details of steps, this is not done well as the follow does not
            // spilt the angle to be rotated into equal portion. The divide is non-linear
            // making the end result looks strange.
            // The point here is, we know what is going on and we can 
            // compute the intermediate rotations

            // begin first rotation aligning Up
            float delta = v1v2Theta / (float)(kNumSteps - 1);
            Quaternion q = Quaternion.AngleAxis(delta, v1v2Axis);
            steps[0].transform.localRotation = q * Init.transform.localRotation;
            steps[0].SetActive(true);
            for (int i = 1; i < kNumSteps - 1; i++)
            {
                steps[i].SetActive(true);

                Vector3 preUp = steps[i - 1].transform.up;
                Vector3 upAxis = Vector3.Cross(preUp, finalUp);
                Debug.Assert(upAxis.magnitude > float.Epsilon);

                float dot = Vector3.Dot(preUp, finalUp);
                if (dot > 1f)
                    dot = 1f;
                if (dot < -1f)
                    dot = -1f;

                float theta = Mathf.Acos(dot) * Mathf.Rad2Deg; // we know v1 and v2 are normalized
                delta = theta * i / (kNumSteps - 2);
                Quaternion alignUp = Quaternion.AngleAxis(delta, upAxis);
                steps[i].transform.localRotation = alignUp * steps[i-1].transform.localRotation;

                // Now, aligns right vector
                Vector3 rAxis = Vector3.Cross(steps[i].transform.right, Final.transform.right);
                dot = Vector3.Dot(steps[i].transform.right, Final.transform.right);
                if (dot > 1f)
                    dot = 1f;
                if (dot < -1f)
                    dot = -1f;

                float rAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                float rDelta = rAngle * i / (kNumSteps - 2);
                Quaternion alignRight = Quaternion.AngleAxis(rDelta, rAxis);
                steps[i].transform.localRotation = alignRight * steps[i].transform.localRotation;
            }
            #endregion
        } else
        {
            for (int i = 0; i < kNumSteps-1; i++)
                steps[i].SetActive(false);
        }

        GameObject lastBlueObj = steps[kNumSteps - 1];
        if (LastUseUnity) {

            Quaternion alignUp = Quaternion.FromToRotation(Vector3.up, Final.transform.up);
            // now align the right vectors
            lastBlueObj.transform.localRotation = alignUp;
            
            Quaternion alignRight = Quaternion.FromToRotation(lastBlueObj.transform.right, Final.transform.right);
            lastBlueObj.transform.localRotation = alignRight * lastBlueObj.transform.localRotation;

        }  else {
            int last = kNumSteps - 1;
            Quaternion alignUp = Quaternion.AngleAxis(v1v2Theta, v1v2Axis);
            steps[last].transform.localRotation = alignUp;

            Vector3 rAxis = Vector3.Cross(steps[last].transform.right, Final.transform.right);
            // Debug.Log ("Axis=" + rAxis + " " + steps[last].transform.up);
            float rAngle = Mathf.Acos(Vector3.Dot(steps[last].transform.right, Final.transform.right)) * Mathf.Rad2Deg;
            Quaternion alignRight = Quaternion.AngleAxis(rAngle, rAxis);
            steps[last].transform.localRotation = alignRight * steps[last].transform.localRotation;
        }
       
    }
}
