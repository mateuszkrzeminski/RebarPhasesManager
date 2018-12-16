using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace RebarPhaseManager.View
{
    public abstract class CommandDialogBox : FrameworkElement, INotifyPropertyChanged
    {
        protected Action<object> execute = null;
        public string Caption { get; set; }
        protected ICommand show;

        public ICommand Show
        {
            get
            {
                if (show == null) show = new RelayCommand(
                o =>
                {
                    ExecuteCommand(CommandBefore, CommandParameter);
                    execute(o);
                    ExecuteCommand(CommandAfter, CommandParameter);
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

        public static DependencyProperty CommandBeforeProperty = DependencyProperty.Register("CommandBefore", typeof(ICommand), typeof(CommandDialogBox));

        public ICommand CommandBefore
        {
            get
            {
                return (ICommand)GetValue(CommandBeforeProperty);
            }
            set
            {
                SetValue(CommandBeforeProperty, value);
            }
        }

        public static DependencyProperty CommandAfterProperty = DependencyProperty.Register("CommandAfter", typeof(ICommand), typeof(CommandDialogBox));

        public ICommand CommandAfter
        {
            get
            {
                return (ICommand)GetValue(CommandAfterProperty);
            }
            set
            {
                SetValue(CommandAfterProperty, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(params string[] propertyName)
        {
            if (PropertyChanged != null)
            {
                foreach (string nazwaWłasności in propertyName)
                    PropertyChanged(this, new PropertyChangedEventArgs(nazwaWłasności));
            }
        }
    }
}
