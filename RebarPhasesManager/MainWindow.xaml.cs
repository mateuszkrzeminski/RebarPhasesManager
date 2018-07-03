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

using Tekla.Structures;
using Tekla.Structures.Model;
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

        private void button_Select_Click(object sender, RoutedEventArgs e)
        {
            phaseManager.ReinitializePhaseViewList();
        }

        private void button_ModifyPhase_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void checkBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            var chkSelectAll = sender as CheckBox;
            var firstCol = dataGrid.Columns.OfType<DataGridTemplateColumn>().FirstOrDefault(c => c.DisplayIndex == 0);
            if (chkSelectAll == null || firstCol == null || dataGrid?.Items == null)
            {
                return;
            }
            foreach (var item in dataGrid.Items)
            {
                var chBx = firstCol.GetCellContent(item);

                if (chBx == null)
                {
                    continue;
                }
                //chBx.IsChecked = chkSelectAll.IsChecked;
            }
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
              phaseManager.SetTemporaryColor();
        }

    }

}
