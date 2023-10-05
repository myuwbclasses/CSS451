using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sv = UnityEditor.SceneVisibilityManager.instance;
        sv.DisablePicking(gameObject, true);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
