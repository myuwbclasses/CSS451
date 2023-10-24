using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject Red_Ref, White_ToCompute;  // defines the initial and final arrows
    public bool LastUseUnity = false;
    public bool ShowSteps = true;
    public bool AlignFrame = false; 
    public GameObject FinalAxisFrame = null;
    private GameObject[] steps; 
    private int kNumSteps = 15;

private void Start()
    {
        Debug.Assert(FinalAxisFrame != null);
        White_ToCompute.GetComponent<Renderer>().material.color = Color.black;
        steps = new GameObject[kNumSteps];
        for (int i = 0; i< 15; i++ )
        {
            steps[i] = Instantiate(Resources.Load("Arrow")) as GameObject;
            steps[i].transform.localScale = White_ToCompute.transform.localScale;
            steps[i].transform.localPosition = White_ToCompute.transform.localPosition;
            steps[i].transform.localRotation = White_ToCompute.transform.localRotation;
        }

        steps[kNumSteps - 1].GetComponent<Renderer>().material.color = Color.blue;
    }

    void Update() {
        if (AlignFrame) 
            AlignBothAxes();            
        else
            AlignOneAxis();
    }

    private void AlignOneAxis() {
        Vector3 v1 = White_ToCompute.transform.up;
        Vector3 v2 = Red_Ref.transform.up;

        Vector3 n = Vector3.Cross(v1, v2);
        float theta = Mathf.Acos(Vector3.Dot(v1, v2)) * Mathf.Rad2Deg; // we know v1 and v2 are normalized

        #region Show the details of rotation 
        float delta = theta / (float)(kNumSteps);
        Quaternion q = Quaternion.AngleAxis(delta, n);
        steps[0].transform.localRotation = q * White_ToCompute.transform.localRotation;
        steps[0].SetActive(ShowSteps);
        for (int i = 1; i < kNumSteps-1; i++)
        {
            steps[i].transform.localRotation = q * steps[i-1].transform.localRotation;
            steps[i].SetActive(ShowSteps);
        }
        #endregion

        Quaternion inOneR = Quaternion.FromToRotation(White_ToCompute.transform.up, Red_Ref.transform.up);
        steps[kNumSteps - 1].transform.localRotation = inOneR;

        // sets the AxisFrame
        FinalAxisFrame.transform.localPosition = steps[kNumSteps-1].transform.localPosition;
        FinalAxisFrame.transform.localRotation = steps[kNumSteps-1].transform.localRotation;
    }

    private void AlignBothAxes()  // this would align the axis frame
    {
        Vector3 initUp = White_ToCompute.transform.up; // Vector3.up; //  
        Vector3 finalUp = Red_Ref.transform.up;
        Vector3 v1v2Axis = Vector3.Cross(initUp, finalUp);
        float v1v2Theta = Mathf.Acos(Vector3.Dot(initUp, finalUp)) * Mathf.Rad2Deg; // we know v1 and v2 are normalized

       // if (v1v2Axis.magnitude < float.Epsilon)
       //   return;

        #region // the details of steps, this is not done well as the follow does not
        // if (ShowSteps)
        {
            // spilt the angle to be rotated into equal portion. The divide is non-linear
            // making the end result looks strange.
            // The point here is, we know what is going on and we can 
            // compute the intermediate rotations

            // begin first rotation aligning Up
            float delta = v1v2Theta / (float)(kNumSteps - 1);
            Quaternion q = Quaternion.AngleAxis(delta, v1v2Axis);
            steps[0].transform.localRotation = q * White_ToCompute.transform.localRotation;
            steps[0].SetActive(ShowSteps);
            for (int i = 1; i < kNumSteps; i++)
            {
                steps[i].SetActive(ShowSteps);

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
                Vector3 rAxis = Vector3.Cross(steps[i].transform.right, Red_Ref.transform.right);
                dot = Vector3.Dot(steps[i].transform.right, Red_Ref.transform.right);
                if (dot > 1f)
                    dot = 1f;
                if (dot < -1f)
                    dot = -1f;

                float rAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                float rDelta = rAngle * i / (kNumSteps - 2);
                Quaternion alignRight = Quaternion.AngleAxis(rDelta, rAxis);
                steps[i].transform.localRotation = alignRight * steps[i].transform.localRotation;
            }
            steps[kNumSteps-1].SetActive(true);
        }
        #endregion
        
        GameObject lastBlueObj = steps[kNumSteps - 1];
        if (LastUseUnity) {
            lastBlueObj.transform.localRotation = Quaternion.LookRotation(lastBlueObj.transform.forward, lastBlueObj.transform.up);
        }  else {
            int last = kNumSteps - 1;
            Quaternion alignUp = Quaternion.AngleAxis(v1v2Theta, v1v2Axis);
            steps[last].transform.localRotation = alignUp;

            Vector3 rAxis = Vector3.Cross(steps[last].transform.right, Red_Ref.transform.right);
            // Vector3 rAxis = steps[last].transform.up;  + or -, may need to flip the direction
            // Debug.Log ("Axis=" + rAxis + " " + steps[last].transform.up);
            float rAngle = Mathf.Acos(Vector3.Dot(steps[last].transform.right, Red_Ref.transform.right)) * Mathf.Rad2Deg;
            Quaternion alignRight = Quaternion.AngleAxis(rAngle, rAxis);
            steps[last].transform.localRotation = alignRight * steps[last].transform.localRotation;
        }
        // sets the AxisFrame
        FinalAxisFrame.transform.localPosition = steps[kNumSteps-1].transform.localPosition;
        FinalAxisFrame.transform.localRotation = steps[kNumSteps-1].transform.localRotation;
    }
}
