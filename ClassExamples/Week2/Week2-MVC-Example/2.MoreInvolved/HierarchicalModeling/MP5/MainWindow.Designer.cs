using System;

namespace ClassExample
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.mStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QuitButton = new System.Windows.Forms.Button();
            this.mSystemTimer = new System.Windows.Forms.Timer(this.components);
            this.mSceneTreeControl = new ClassExample.SceneTreeControl();
            this.mSmallView = new ClassExample.ViewControl();
            this.mMainDrawArea = new ClassExample.ViewControlWithMouse();
            this.SuspendLayout();
            // 
            // mStatus
            // 
            this.mStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mStatus.Location = new System.Drawing.Point(637, 697);
            this.mStatus.Name = "mStatus";
            this.mStatus.ReadOnly = true;
            this.mStatus.Size = new System.Drawing.Size(328, 22);
            this.mStatus.TabIndex = 11;
            this.mStatus.Text = "All OK!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 701);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Status";
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(956, 696);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(75, 23);
            this.QuitButton.TabIndex = 12;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // mSystemTimer
            // 
            this.mSystemTimer.Interval = 25;
            this.mSystemTimer.Tick += new System.EventHandler(this.mSystemTimer_Tick);
            // 
            // mSceneTreeControl
            // 
            this.mSceneTreeControl.Location = new System.Drawing.Point(596, 3);
            this.mSceneTreeControl.Name = "mSceneTreeControl";
            this.mSceneTreeControl.Size = new System.Drawing.Size(449, 366);
            this.mSceneTreeControl.TabIndex = 22;
            // 
            // mSmallView
            // 
            this.mSmallView.Location = new System.Drawing.Point(596, 359);
            this.mSmallView.Model = null;
            this.mSmallView.Name = "mSmallView";
            this.mSmallView.Size = new System.Drawing.Size(435, 332);
            this.mSmallView.TabIndex = 21;
            this.mSmallView.Text = "Small View";
            // 
            // mMainDrawArea
            // 
            this.mMainDrawArea.Location = new System.Drawing.Point(-3, 3);
            this.mMainDrawArea.Model = null;
            this.mMainDrawArea.Name = "mMainDrawArea";
            this.mMainDrawArea.Size = new System.Drawing.Size(593, 716);
            this.mMainDrawArea.TabIndex = 0;
            this.mMainDrawArea.Text = "Main Window Display";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 720);
            this.Controls.Add(this.mSceneTreeControl);
            this.Controls.Add(this.mSmallView);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.mStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mMainDrawArea);
            this.Name = "MainWindow";
            this.Text = "Kelvin MP5 Attempt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TextBox mStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button QuitButton;
        private ViewControlWithMouse mMainDrawArea;
        private System.Windows.Forms.Timer mSystemTimer;
        private ViewControl mSmallView;
        private SceneTreeControl mSceneTreeControl;
    }
}