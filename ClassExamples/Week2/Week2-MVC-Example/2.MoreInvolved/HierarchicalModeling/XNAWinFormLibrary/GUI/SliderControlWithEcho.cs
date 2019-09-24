using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms.Layout;


namespace XNAWinFormLibrary
{

    /// <summary>
    /// A Horizontal SliderBar with built in EchoLabel
    /// </summary>
    public class SliderControlWithEcho : System.Windows.Forms.TrackBar
    {
        // Value used to convert from int to float for the TrackBar values
        private const int SLIDER_RESOLUTION = 100000;

        // new data type
        public delegate void OnNewSliderValueDelegate(float newValue);
        public event OnNewSliderValueDelegate OnNewSliderValue;

        private float m_minimum_value;
        private float m_value_range;

        // EchoLabel for the SliderBar
        private System.Windows.Forms.Label sliderEcho;
        private System.Windows.Forms.Label sliderLabel;
        private Size sliderBarSize;

        // Constructor
        public SliderControlWithEcho()
        {
            OnNewSliderValue = null;

            this.sliderBarSize = this.Size;
            this.AutoSize = false;
            this.TickStyle = TickStyle.None;

            const int useYOffset = 20;
            const float hRatio = 0.4f;


            // label
            this.sliderLabel = new Label();
            //this.sliderLabel.BorderStyle = BorderStyle.Fixed3D;
            this.sliderLabel.BackColor = Color.LightBlue;
            this.sliderLabel.Text = "??";
            this.sliderLabel.TextAlign = ContentAlignment.MiddleRight;
            this.sliderLabel.Size = new Size(sliderBarSize.Width / 5, (int)(sliderBarSize.Height * hRatio));

            Point location = this.Location;
            location.X = 5;
            location.Y = useYOffset;

            this.sliderLabel.Location = location;
            this.Controls.Add(sliderLabel);

            // echo
            this.sliderEcho = new Label();
            this.sliderEcho.BorderStyle = BorderStyle.Fixed3D;
            this.sliderEcho.Text = "0.00";
            this.sliderEcho.TextAlign = ContentAlignment.MiddleRight;
            this.sliderEcho.Size = new Size((int)(sliderBarSize.Width / 2.5f), (int)(sliderBarSize.Height * hRatio));

            location = this.Location;
            location.X = sliderLabel.Location.X + sliderLabel.Size.Width;
            location.Y = useYOffset;
            this.sliderEcho.Location = location;
            this.Controls.Add(sliderEcho);
            

            m_minimum_value = this.Minimum;
            m_value_range = this.Maximum - this.Minimum;
            this.SetRange(0, SLIDER_RESOLUTION);
        }

        // This is the function that must be used to change the orientation
        // of the sliderBar if they still want it to look nice.  
        // It would be more appropiate to do this by simply using the [Design]Window properties
        // to change the orientation, but there isn't an event to check for OnOrientationChange()
        // and currently I don't know how to add one in, and that's why I did it this way.
        public void OrientationToVertical(int sliderHeight)
        {
            const int xOffset = 20;

            this.Orientation = Orientation.Vertical;
            Size verticalSize = new Size(sliderBarSize.Height, sliderBarSize.Width);
            this.Size = new Size(verticalSize.Width + 40, sliderHeight);

            this.AutoSize = false;

            // label
            this.sliderLabel.Dispose();
            this.sliderLabel = new Label();
            //this.sliderLabel.BorderStyle = BorderStyle.Fixed3D;
            this.sliderLabel.BackColor = Color.LightBlue;
            this.sliderLabel.Text = "??";
            this.sliderLabel.TextAlign = ContentAlignment.MiddleRight;
            this.sliderLabel.Size = new Size(sliderBarSize.Width / 5, sliderBarSize.Height/2);

            Point location = this.Location;
            location.X = xOffset;
            location.Y = 5;

            this.sliderLabel.Location = location;
            this.Controls.Add(sliderLabel);

            // echo
            this.sliderEcho.Dispose();  // Destroy the old label
            this.sliderEcho = new Label();
            this.sliderEcho.BorderStyle = BorderStyle.Fixed3D;
            this.sliderEcho.TextAlign = ContentAlignment.MiddleRight;
            this.sliderEcho.Size = new Size(sliderBarSize.Width / 2, sliderBarSize.Height / 2);

            location = this.Location;
            location.X = xOffset;
            location.Y = sliderLabel.Location.Y+sliderLabel.Size.Height;

            this.sliderEcho.Location = location;
            this.Controls.Add(sliderEcho);


            this.UpdateText();
        }

        // 
        /// <summary>
        /// Used to Update the range values used to convert from float/int
        /// Must be called to update information if user changes values from
        /// the [Design]Form window 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="initial"></param>
        public void ChangeRangeValues(float min, float max, float initial)
        {
            m_minimum_value = min;
            m_value_range = max - min;
            this.SetRange(0, SLIDER_RESOLUTION);
            SetSliderValue(initial);
            UpdateText();
        }

        // 
        /// <summary>
        /// Used to Update the label text 
        /// </summary>
        public void UpdateText()
        {
            float userValue = GetSliderValue();
            this.sliderEcho.Text = userValue.ToString("0.00");
        }

        // 
        /// <summary>
        /// Given the userValue set the trackbar's value 
        /// </summary>
        /// <param name="userValue"></param>
        /// <returns></returns>
        public bool SetSliderValue(float userValue)
        {
            if (userValue < m_minimum_value || userValue > (m_minimum_value + m_value_range))
            {
                return false;
            }
            int controlPosition = ConvertUserValueToSliderPosition(userValue);
            this.Value = controlPosition;
            this.Update();
            UpdateText();
            return true;
        }

        // 
        /// <summary>
        /// Used to get the float value/position of the trackbar 
        /// </summary>
        /// <returns></returns>
        public float GetSliderValue()
        {
            int slider_position = this.Value;
            return ConvertSliderPositionToUserValue(slider_position);
        }

        /// <summary>
        /// Sets the label of the control
        /// </summary>
        /// <param name="label"></param>
        public void SetLabel(String label)
        {
            sliderLabel.Text = label;
        }

        // Function for converting the int position of the slider value
        // to a float value
        protected float ConvertSliderPositionToUserValue(int position)
        {
            return m_minimum_value + ((float)(position) / (float)(SLIDER_RESOLUTION) * m_value_range);
        }

        // Function for converting the float value back to the int trackbar position
        protected int ConvertUserValueToSliderPosition(float userValue)
        {
            int slider_position = (int)(((userValue - m_minimum_value) * SLIDER_RESOLUTION / m_value_range) + 0.5f);
            return slider_position;
        }

        // Currently just implements the base class method.
        // Contemplated on having the label resize with
        // the trackbar in some way
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        // OnScroll method echos the values to the echo label
        protected override void OnScroll(EventArgs e)
        {
            base.OnScroll(e);

            float userValue = GetSliderValue();
            this.sliderEcho.Text = userValue.ToString("0.00");

            if (null != OnNewSliderValue)
                OnNewSliderValue(userValue);
        }

        // Currently the same implementation as the base class
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            // allow normal control painting to occur first
            base.OnPaint(e);
        }

    }
}

