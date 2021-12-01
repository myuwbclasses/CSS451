using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNodeControl : MonoBehaviour {
    public Dropdown TheMenu = null;
    public SceneNode TheRoot = null;
    public XfromControl XformControl = null;

    const string kChildSpace = "  ";
    List<Dropdown.OptionData> mSelectMenuOptions = new List<Dropdown.OptionData>();
    List<Transform> mSelectedTransform = new List<Transform>();    

    // Use this for initialization
    void Start () {
        Debug.Assert(TheMenu != null);
        Debug.Assert(TheRoot != null);
        Debug.Assert(XformControl != null);

        mSelectMenuOptions.Add(new Dropdown.OptionData(TheRoot.transform.name));
        mSelectedTransform.Add(TheRoot.transform);
        GetChildrenNames("", TheRoot.transform);
        TheMenu.AddOptions(mSelectMenuOptions);
        TheMenu.onValueChanged.AddListener(SelectionChange);

        XformControl.SetSelectedObject(TheRoot.transform);
    }

    void GetChildrenNames(string blanks, Transform node)
    {
        string space = blanks + kChildSpace;
        for (int i = node.childCount - 1; i >= 0; i--)
        {
            Transform child = node.GetChild(i);
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                mSelectMenuOptions.Add(new Dropdown.OptionData(space + child.name));
                mSelectedTransform.Add(child);
                GetChildrenNames(blanks + kChildSpace, child);
            }
        }
    }

    void SelectionChange(int index)
    {
        XformControl.SetSelectedObject(mSelectedTransform[index]);
    }
}
