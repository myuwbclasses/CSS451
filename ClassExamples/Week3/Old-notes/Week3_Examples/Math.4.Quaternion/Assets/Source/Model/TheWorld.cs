using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject Init, Final;  // defines the initial and final arrows
    public bool LastUseUnity = true;
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
        Vector3 v1 = Init.transform.up; // Vector3.up; //  
        Vector3 v2 = Final.transform.up;

        Vector3 n = Vector3.Cross(v1, v2);
        float theta = Mathf.Acos(Vector3.Dot(v1, v2)) * Mathf.Rad2Deg; // we know v1 and v2 are normalized
        float delta = theta / (float)(kNumSteps-1);
        for (int i = 0; i < kNumSteps-1; i++)
        {
            Quaternion q = Quaternion.AngleAxis(i * delta, n);
            steps[i].transform.localRotation = q * Init.transform.localRotation;
        }
        Quaternion inOneR;
        if (LastUseUnity) {
            inOneR = Quaternion.FromToRotation(Vector3.up, Final.transform.up);  // One step is from initial position of the object 
        }  else {
            inOneR = Quaternion.AngleAxis(theta, n);
        }
       steps[kNumSteps - 1].transform.localRotation = inOneR;
    }
}
