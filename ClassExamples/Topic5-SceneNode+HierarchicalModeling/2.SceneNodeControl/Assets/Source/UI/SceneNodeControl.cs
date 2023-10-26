using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneNodeControl : MonoBehaviour {
    public TMP_Dropdown TheMenu = null;
    public SceneNode TheRoot = null;
    public XfromControl XformControl = null;

    const string kChildSpace = " ";
    List<TMP_Dropdown.OptionData> mSelectMenuOptions = null;
    List<Transform> mSelectedTransform = new List<Transform>();

    // Use this for initialization
    void Start () {
        Debug.Assert(TheMenu != null);
        Debug.Assert(TheRoot != null);
        Debug.Assert(XformControl != null);

        mSelectMenuOptions = new List<TMP_Dropdown.OptionData>();
        mSelectMenuOptions.Add(new TMP_Dropdown.OptionData(TheRoot.transform.name));
        mSelectedTransform.Add(TheRoot.transform);
        GetChildrenNames("", TheRoot);
        TheMenu.AddOptions(mSelectMenuOptions);
        TheMenu.onValueChanged.AddListener(SelectionChange);

        XformControl.SetSelectedObject(TheRoot.transform);
    }

    void GetChildrenNames(string blanks, SceneNode node)
    {
        string space = blanks + kChildSpace;
        List<SceneNode> children = node.ChildrenList;
        for (int i = children.Count - 1; i >= 0; i--)
        {
            SceneNode cn = children[i];
            TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData(space + children[i].transform.name);
            // Debug.Log("Add: " + space + child.name);
            mSelectMenuOptions.Add(d);
            mSelectedTransform.Add(children[i].transform);
            GetChildrenNames(space, children[i]);
        }
    }

    void SelectionChange(int index)
    {
        XformControl.SetSelectedObject(mSelectedTransform[index]);
    }
}
