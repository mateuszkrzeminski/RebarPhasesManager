using System;

using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.ComponentModel;

using Tekla.Structures.Model.UI;

namespace RebarPhaseManager
{
    public class TeklaColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Tekla.Structures.Model.UI.Color teklaColor = value as Tekla.Structures.Model.UI.Color;
            System.Windows.Media.Color color = new System.Windows.Media.Color();
            color.ScA = (float)teklaColor.Transparency;
            color.ScR = (float)teklaColor.Red;
            color.ScG = (float)teklaColor.Green;
            color.ScB = (float)teklaColor.Blue;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
