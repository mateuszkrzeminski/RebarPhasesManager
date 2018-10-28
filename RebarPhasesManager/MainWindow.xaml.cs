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



namespace RebarPhasesManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RebarPhasesManager.Model.MainModel test;

        public MainWindow()
        {
            InitializeComponent();
            test = new Model.MainModel();
        }

        private void button_Select_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_ModifyPhase_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void checkBoxHeader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {

        }

    }

}
