namespace ClassExample
{
    partial class XformInfoControl
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mT = new System.Windows.Forms.RadioButton();
            this.mR = new System.Windows.Forms.RadioButton();
            this.mS = new System.Windows.Forms.RadioButton();
            this.Reset = new System.Windows.Forms.Button();
            this.mP = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mT
            // 
            this.mT.AutoSize = true;
            this.mT.Location = new System.Drawing.Point(9, 151);
            this.mT.Name = "mT";
            this.mT.Size = new System.Drawing.Size(32, 17);
            this.mT.TabIndex = 4;
            this.mT.TabStop = true;
            this.mT.Text = "T";
            this.mT.UseVisualStyleBackColor = true;
            this.mT.CheckedChanged += new System.EventHandler(this.mT_CheckedChanged);
            // 
            // mR
            // 
            this.mR.AutoSize = true;
            this.mR.Location = new System.Drawing.Point(47, 151);
            this.mR.Name = "mR";
            this.mR.Size = new System.Drawing.Size(33, 17);
            this.mR.TabIndex = 5;
            this.mR.TabStop = true;
            this.mR.Text = "R";
            this.mR.UseVisualStyleBackColor = true;
            this.mR.CheckedChanged += new System.EventHandler(this.mR_CheckedChanged);
            // 
            // mS
            // 
            this.mS.AutoSize = true;
            this.mS.Location = new System.Drawing.Point(86, 151);
            this.mS.Name = "mS";
            this.mS.Size = new System.Drawing.Size(32, 17);
            this.mS.TabIndex = 6;
            this.mS.TabStop = true;
            this.mS.Text = "S";
            this.mS.UseVisualStyleBackColor = true;
            this.mS.CheckedChanged += new System.EventHandler(this.mS_CheckedChanged);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(159, 149);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(51, 22);
            this.Reset.TabIndex = 8;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // mP
            // 
            this.mP.AutoSize = true;
            this.mP.Location = new System.Drawing.Point(124, 151);
            this.mP.Name = "mP";
            this.mP.Size = new System.Drawing.Size(32, 17);
            this.mP.TabIndex = 9;
            this.mP.TabStop = true;
            this.mP.Text = "P";
            this.mP.UseVisualStyleBackColor = true;
            this.mP.CheckedChanged += new System.EventHandler(this.mP_CheckedChanged);
            // 
            // XformInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.mP);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.mS);
            this.Controls.Add(this.mR);
            this.Controls.Add(this.mT);
            this.Name = "XformInfoControl";
            this.Size = new System.Drawing.Size(216, 188);
            this.Controls.SetChildIndex(this.mT, 0);
            this.Controls.SetChildIndex(this.mR, 0);
            this.Controls.SetChildIndex(this.mS, 0);
            this.Controls.SetChildIndex(this.Reset, 0);
            this.Controls.SetChildIndex(this.mP, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion 

        private System.Windows.Forms.RadioButton mT;
        private System.Windows.Forms.RadioButton mR;
        private System.Windows.Forms.RadioButton mS;
        private System.Windows.Forms.RadioButton mP;
        private System.Windows.Forms.Button Reset;

    }
}
