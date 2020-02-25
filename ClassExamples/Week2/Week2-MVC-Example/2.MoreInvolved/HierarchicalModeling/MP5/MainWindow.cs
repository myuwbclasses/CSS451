using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassExample
{
    public partial class MainWindow : Form
    {
        MyModel mTheWorld;

        #region a Cheesy backdoor for allowing status update from anywere in the project
        private static MainWindow sMainWindow;
        public static void EchoToStatus(String msg)
        {
            sMainWindow.mStatus.Text = msg;
        }
        #endregion

        //instantiate the model to be drawn
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Creation and initialization of instances should be done in OnCreateControl, by this time 
        /// we can count on all other controls (GUI/Windows) are properly created.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            mTheWorld = new MyModel();
            mMainDrawArea.Model = mTheWorld;
            mMainDrawArea.SetOtherViewCamera(mSmallView.ViewCamera);
            mSmallView.Model = mTheWorld;
            mTheWorld.SetCameraToControl(mSmallView.ViewCamera);

            // to support status update from anywhere in the source code system
            MainWindow.sMainWindow = this;

            mSystemTimer.Enabled = true;

            mSceneTreeControl.InitializeControls(mTheWorld);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// This is called once every 25 mSec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mSystemTimer_Tick(object sender, EventArgs e)
        {
            mMainDrawArea.RedrawWindow();
            mSmallView.RedrawWindow();

            mTheWorld.UpdateModel();
        }

       

    }
}
