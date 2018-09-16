using System.ComponentModel;

namespace RebarPhasesManager.ViewModel
{
    public abstract class ObservedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(params string[] propertyName)
        {
            if (PropertyChanged != null)
            {
                foreach (string nazwaWłasności in propertyName)
                    PropertyChanged(this, new PropertyChangedEventArgs(nazwaWłasności));
            }
        }
    }
}
