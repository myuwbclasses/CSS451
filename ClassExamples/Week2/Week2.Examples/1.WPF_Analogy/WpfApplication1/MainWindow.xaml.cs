using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            mButton.Height = 100;
            mButton.Click += button_Click;  // either do it this way or from the GUI
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mButton.Height = mButton.Height + 10;
            Console.WriteLine("Button pressed: Height=" + mButton.Height);
        }

        private void mSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine("Slider value=" + e.NewValue);
            mButton.Height = (int) e.NewValue;
        }
    }
}
