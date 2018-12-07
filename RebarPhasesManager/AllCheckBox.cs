using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RebarPhaseManager
{
    class AllCheckBox : CheckBox
    {
        public static readonly DependencyProperty InvertCheckStateOrderProperty =
    DependencyProperty.Register("InvertCheckStateOrder", typeof(bool), typeof(AllCheckBox), new UIPropertyMetadata(false));

        public bool InvertCheckStateOrder
        {
            get { return (bool)GetValue(InvertCheckStateOrderProperty); }
            set { SetValue(InvertCheckStateOrderProperty, value); }
        }

        protected override void OnToggle()
        {
            if (this.InvertCheckStateOrder)
            {
                if (this.IsChecked == true)
                {
                    this.IsChecked = false;
                }
                else if (this.IsChecked == false)
                {
                    this.IsChecked = this.IsThreeState ? null : (bool?)true;
                }
                else
                {
                    this.IsChecked = true;
                }
            }
            else
            {
                base.OnToggle();
            }
        }

    }
}
