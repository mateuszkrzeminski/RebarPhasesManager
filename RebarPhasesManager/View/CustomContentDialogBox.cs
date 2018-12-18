using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Controls;

namespace RebarPhaseManager.View
{
    [ContentProperty("WindowContent")]
    public class CustomContentDialogBox : FrameworkElement
    {
        protected Action<object> execute = null;
        bool? LastResult;

        public string Caption { get; set; }
        public double WindowWidth { get; set; } = 640;
        public double WindowHeight { get; set; } = 480;
        public object WindowContent { get; set; } = null;

        private static Window window = null;

        public CustomContentDialogBox()
        {
            execute =
            o =>
            {
                if (window == null)
                {
                    window = new Window();
                    window.Width = WindowWidth;
                    window.Height = WindowHeight;
                    if (Caption != null) window.Title = Caption;
                    window.Content = WindowContent;
                    LastResult = window.ShowDialog();
                    window = null;
                }

            };
        }

        protected ICommand show;

        public ICommand Show
        {
            get
            {
                if (show == null) show = new RelayCommand(
                o =>
                {
                    execute(o);
                });
                return show;
            }
        }

        public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(CommandDialogBox));

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        protected static void ExecuteCommand(ICommand command, object commandParameter)
        {
            if (command != null)
                if (command.CanExecute(commandParameter))
                    command.Execute(commandParameter);
        }

        public static bool? GetCustomContentDialogResult(DependencyObject d)
        {
            return (bool?)d.GetValue(DialogResultProperty);
        }

        public static void SetCustomContentDialogResult(DependencyObject d, bool? value)
        {
            d.SetValue(DialogResultProperty, value);
        }

        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(CustomContentDialogBox), new PropertyMetadata(null, DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool? dialogResult = (bool?)e.NewValue;
            if (d is Button)
            {
                Button button = d as Button;
                button.Click +=
                (object sender, RoutedEventArgs _e) =>
                {
                    window.DialogResult = dialogResult;
                };
            }
        }

    }
}
