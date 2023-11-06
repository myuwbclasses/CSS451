using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewNDCControl : MonoBehaviour
{
    public GameObject Org;
    public bool ShowNDC = false;
    public FrustumInSpace frustum = null;
    public ShowInView_NDC_Space view = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Org != null);
        Debug.Assert(frustum != null);
        Debug.Assert(view != null);
        
    }

    // Update is called once per frame
    void Update()
    {
        frustum.SetToNDCSpace(ShowNDC);
        view.SetToNDCSpace(ShowNDC);
        if (ShowNDC) {
            Org.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        } else {
            Org.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        
    }
}
