namespace XNAWinFormLibrary
{
    partial class TripleSliderControl
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
            this.mTitle = new System.Windows.Forms.GroupBox();
            this.mBottomSlider = new XNAWinFormLibrary.SliderControlWithEcho();
            this.mMidSlider = new XNAWinFormLibrary.SliderControlWithEcho();
            this.mTopSlider = new XNAWinFormLibrary.SliderControlWithEcho();
            this.mTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mBottomSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mMidSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTopSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // mTitle
            // 
            this.mTitle.Controls.Add(this.mBottomSlider);
            this.mTitle.Controls.Add(this.mMidSlider);
            this.mTitle.Controls.Add(this.mTopSlider);
            this.mTitle.Location = new System.Drawing.Point(0, 0);
            this.mTitle.Name = "mTitle";
            this.mTitle.Size = new System.Drawing.Size(210, 145);
            this.mTitle.TabIndex = 3;
            this.mTitle.TabStop = false;
            this.mTitle.Text = "Triple Slider Control";
            // 
            // mBottomSlider
            // 
            this.mBottomSlider.AutoSize = false;
            this.mBottomSlider.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mBottomSlider.Location = new System.Drawing.Point(6, 102);
            this.mBottomSlider.Maximum = 100000;
            this.mBottomSlider.Name = "mBottomSlider";
            this.mBottomSlider.Size = new System.Drawing.Size(200, 41);
            this.mBottomSlider.TabIndex = 2;
            this.mBottomSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // mMidSlider
            // 
            this.mMidSlider.AutoSize = false;
            this.mMidSlider.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mMidSlider.Location = new System.Drawing.Point(4, 57);
            this.mMidSlider.Maximum = 100000;
            this.mMidSlider.Name = "mMidSlider";
            this.mMidSlider.Size = new System.Drawing.Size(200, 41);
            this.mMidSlider.TabIndex = 1;
            this.mMidSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // mTopSlider
            // 
            this.mTopSlider.AutoSize = false;
            this.mTopSlider.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mTopSlider.Location = new System.Drawing.Point(6, 13);
            this.mTopSlider.Maximum = 100000;
            this.mTopSlider.Name = "mTopSlider";
            this.mTopSlider.Size = new System.Drawing.Size(200, 41);
            this.mTopSlider.TabIndex = 0;
            this.mTopSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // TripleSliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.mTitle);
            this.Name = "TripleSliderControl";
            this.Size = new System.Drawing.Size(211, 146);
            this.mTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mBottomSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mMidSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTopSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SliderControlWithEcho mTopSlider;
        private SliderControlWithEcho mMidSlider;
        private SliderControlWithEcho mBottomSlider;
        private System.Windows.Forms.GroupBox mTitle;
    }
}
