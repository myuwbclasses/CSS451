using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public GameObject Init, Final;  // defines the initial and final arrows
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
        Vector3 v1 = Init.transform.up;
        Vector3 v2 = Final.transform.up;

        Vector3 n = Vector3.Cross(v1, v2);
        float theta = Mathf.Acos(Vector3.Dot(v1, v2)) * Mathf.Rad2Deg; // we know v1 and v2 are normalized
        float delta = theta / (float)(kNumSteps);
        Quaternion q = Quaternion.AngleAxis(delta, n);
        steps[0].transform.localRotation = q * Init.transform.localRotation;
        for (int i = 1; i < kNumSteps-1; i++)
        {
            steps[i].transform.localRotation = q * steps[i-1].transform.localRotation;
        }

        Quaternion inOneR = Quaternion.FromToRotation(Init.transform.up, Final.transform.up);
        steps[kNumSteps - 1].transform.localRotation = inOneR;        
        
    }
}
