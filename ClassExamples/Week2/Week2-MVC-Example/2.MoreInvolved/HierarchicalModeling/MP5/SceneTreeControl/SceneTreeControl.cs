using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

using UWB_GraphicsLibrary;

namespace ClassExample
{
	public partial class SceneTreeControl : UserControl
	{
        SceneNode mCurrentSelectedNode;

		public SceneTreeControl()
		{
            InitializeComponent();
		}

        public void InitializeControls(MyModel m)
        {
            mCurrentSelectedNode = m.GetRootNode();
            BuildSceneTree(mCurrentSelectedNode);
            SelectedNodeSetControl();
        }

        private void SelectedNodeSetControl()
        {
            // set controls with node's XformInfo
            mXformControl.SetXform(mCurrentSelectedNode.Xform);

            mShowNodePivot.Checked = mCurrentSelectedNode.ShowPivot;
            
            // Get the zero-th shape
            int n = mCurrentSelectedNode.GetNumShapesInNode();
            if (n > 0)
            {
                // sets range of selectable shapes
                mShapeAttributes.Enabled = true;
                mNthShape.Maximum = n - 1;
                mNthShape.Value = 0;

                // sets color
                Shape s = mCurrentSelectedNode.GetShapeAtIndex(0);
                mColorControl.SetShape(s);
            }
            else
            {
                mShapeAttributes.Enabled = false;
            }
        }

        private void BuildSceneTree(SceneNode sceneNode) {
            TreeControl.BeginUpdate();
               TreeControl.Nodes.Add(sceneNode.Name);
               TreeNode n = TreeControl.Nodes[0];
               n.Tag = (object)sceneNode;
               sceneTreeHelper(sceneNode, ref n);
            TreeControl.EndUpdate();
        }

        private void sceneTreeHelper(SceneNode sceneNode, ref TreeNode parent)
        {
            List<SceneNode> kids = sceneNode.GetChildren();
            for (int i = 0; i<kids.Count; i++)
            {
                var n = kids[i];
                parent.Nodes.Add(n.Name);
                TreeNode p = parent.Nodes[i];
                p.Tag = (object)n;
                sceneTreeHelper(n, ref p);
            }
        }

        private void TreeControl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            mCurrentSelectedNode = (SceneNode)e.Node.Tag;
            SelectedNodeSetControl();
        }

        private void mSelectShape_ValueChanged(object sender, EventArgs e)
        {
            if (mShapeAttributes.Enabled)
            {
                Shape s = mCurrentSelectedNode.GetShapeAtIndex((int)mNthShape.Value);
                mColorControl.SetShape(s);
            }
        }

        private void mShowPivot_CheckedChanged(object sender, EventArgs e)
        {
            if (mShapeAttributes.Enabled)
                mCurrentSelectedNode.GetShapeAtIndex((int)mNthShape.Value).DrawPivot = mShowPivot.Checked;
        }

        private void mShowNodePivot_CheckedChanged(object sender, EventArgs e)
        {
            mCurrentSelectedNode.ShowPivot = mShowNodePivot.Checked;
        }
 
	}
}
