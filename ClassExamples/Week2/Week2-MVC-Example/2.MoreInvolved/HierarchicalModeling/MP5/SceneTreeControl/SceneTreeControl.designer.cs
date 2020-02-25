namespace ClassExample
{
	partial class SceneTreeControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.TreeControl = new System.Windows.Forms.TreeView();
            this.mNthShape = new System.Windows.Forms.NumericUpDown();
            this.mShowPivot = new System.Windows.Forms.CheckBox();
            this.mShapeAttributes = new System.Windows.Forms.GroupBox();
            this.mXformControl = new ClassExample.XformInfoControl();
            this.mColorControl = new ClassExample.ShapeColorControl();
            this.mShowNodePivot = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mNthShape)).BeginInit();
            this.mShapeAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeControl
            // 
            this.TreeControl.Location = new System.Drawing.Point(2, 0);
            this.TreeControl.Margin = new System.Windows.Forms.Padding(2);
            this.TreeControl.Name = "TreeControl";
            this.TreeControl.Size = new System.Drawing.Size(281, 178);
            this.TreeControl.TabIndex = 2;
            this.TreeControl.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeControl_AfterSelect);
            // 
            // mNthShape
            // 
            this.mNthShape.Location = new System.Drawing.Point(167, 19);
            this.mNthShape.Name = "mNthShape";
            this.mNthShape.Size = new System.Drawing.Size(45, 20);
            this.mNthShape.TabIndex = 8;
            this.mNthShape.ValueChanged += new System.EventHandler(this.mSelectShape_ValueChanged);
            // 
            // mShowPivot
            // 
            this.mShowPivot.AutoSize = true;
            this.mShowPivot.Location = new System.Drawing.Point(6, 22);
            this.mShowPivot.Name = "mShowPivot";
            this.mShowPivot.Size = new System.Drawing.Size(80, 17);
            this.mShowPivot.TabIndex = 9;
            this.mShowPivot.Text = "Show Pivot";
            this.mShowPivot.UseVisualStyleBackColor = true;
            this.mShowPivot.CheckedChanged += new System.EventHandler(this.mShowPivot_CheckedChanged);
            // 
            // mShapeAttributes
            // 
            this.mShapeAttributes.BackColor = System.Drawing.SystemColors.Info;
            this.mShapeAttributes.Controls.Add(this.mShowPivot);
            this.mShapeAttributes.Controls.Add(this.mColorControl);
            this.mShapeAttributes.Controls.Add(this.mNthShape);
            this.mShapeAttributes.Location = new System.Drawing.Point(221, 167);
            this.mShapeAttributes.Name = "mShapeAttributes";
            this.mShapeAttributes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mShapeAttributes.Size = new System.Drawing.Size(218, 195);
            this.mShapeAttributes.TabIndex = 10;
            this.mShapeAttributes.TabStop = false;
            this.mShapeAttributes.Text = "Selected Shape";
            // 
            // mXformControl
            // 
            this.mXformControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mXformControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mXformControl.Location = new System.Drawing.Point(3, 175);
            this.mXformControl.Name = "mXformControl";
            this.mXformControl.Size = new System.Drawing.Size(222, 184);
            this.mXformControl.TabIndex = 1;
            // 
            // mColorControl
            // 
            this.mColorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mColorControl.Location = new System.Drawing.Point(3, 42);
            this.mColorControl.Name = "mColorControl";
            this.mColorControl.Size = new System.Drawing.Size(215, 153);
            this.mColorControl.TabIndex = 3;
            // 
            // mShowNodePivot
            // 
            this.mShowNodePivot.AutoSize = true;
            this.mShowNodePivot.Location = new System.Drawing.Point(288, 0);
            this.mShowNodePivot.Name = "mShowNodePivot";
            this.mShowNodePivot.Size = new System.Drawing.Size(109, 17);
            this.mShowNodePivot.TabIndex = 11;
            this.mShowNodePivot.Text = "Show Node Pivot";
            this.mShowNodePivot.UseVisualStyleBackColor = true;
            this.mShowNodePivot.CheckedChanged += new System.EventHandler(this.mShowNodePivot_CheckedChanged);
            // 
            // SceneTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mShowNodePivot);
            this.Controls.Add(this.mXformControl);
            this.Controls.Add(this.TreeControl);
            this.Controls.Add(this.mShapeAttributes);
            this.Name = "SceneTreeControl";
            this.Size = new System.Drawing.Size(449, 366);
            ((System.ComponentModel.ISupportInitialize)(this.mNthShape)).EndInit();
            this.mShapeAttributes.ResumeLayout(false);
            this.mShapeAttributes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private XformInfoControl mXformControl;
        private System.Windows.Forms.TreeView TreeControl;
        private ShapeColorControl mColorControl;
        private System.Windows.Forms.NumericUpDown mNthShape;
        private System.Windows.Forms.CheckBox mShowPivot;
        private System.Windows.Forms.GroupBox mShapeAttributes;
        private System.Windows.Forms.CheckBox mShowNodePivot;

    }
}
