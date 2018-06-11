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

using Tekla.Structures.Model.UI;

namespace RebarPhasesManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PhaseManager phaseManager;

        public MainWindow()
        {
            InitializeComponent();
            phaseManager = FindResource("phaseManager") as PhaseManager;
        }

        private void button_Set_Click(object sender, RoutedEventArgs e)
        {
            phaseManager.SetTemporaryColor();

        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            ModelObjectVisualization.ClearAllTemporaryStates();

        }

        public void Checked(object sender, RoutedEventArgs e)
        {
              phaseManager.SetTemporaryColor();
        }

        public void Unchecked(object sender, RoutedEventArgs e)
        {
              phaseManager.SetTemporaryColor();
        }

    }
}
