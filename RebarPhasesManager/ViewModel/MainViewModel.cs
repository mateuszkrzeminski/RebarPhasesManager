using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RebarPhasesManager.Model;


namespace RebarPhasesManager.ViewModel
{
    class MainViewModel : ObservedObject
    {
        #region Construction
        public MainViewModel()
        {
            _mainModel.PhaseItemsList.CollectionChanged += phaseItemListSynchronization;
        }
        #endregion

        #region Members
        private MainModel _mainModel = new MainModel();
        private bool? allVisible;
        private bool allVisibleChanging;
        #endregion

        #region Properties
        public ObservableCollection<PhaseItemViewModel> PhaseItemsViewModelList { get; } = new ObservableCollection<PhaseItemViewModel>();
        public PhaseItemViewModel SelectedPhaseItem { get; set; }

        public bool? AllVisible
        {
            get { return allVisible; }
            set
            {
                if (allVisible != value)
                    allVisible = value;
                AllSelectedChanged();
                OnPropertyChanged("AllVisible");
            }
        }
        #endregion

            #region Commands
        private ICommand addRebars;
        public ICommand AddRebars
        {
            get
            {
                if (addRebars == null)
                    addRebars = new RelayCommand(o => _mainModel.AddRebars());
                return addRebars;
            }
        }

        private ICommand removeRebars;
        public ICommand RemoveRebars
        {
            get
            {
                if (removeRebars == null)
                    removeRebars = new RelayCommand(o => _mainModel.RemoveRebars());
                return removeRebars;
            }
        }

        private ICommand modifyPhase;
        public ICommand ModifyPhase
        {
            get
            {
                if (modifyPhase == null)
                    modifyPhase = new RelayCommand(o => _mainModel.ModifyPhase(), o => SelectedPhaseItem != null);
                return modifyPhase;
            }
        }

        private ICommand selectByPhase;
        public ICommand SelectByPhase
        {
            get
            {
                if (selectByPhase == null)
                    selectByPhase = new RelayCommand(o => _mainModel.SelectByPhase(), o => SelectedPhaseItem != null);
                return selectByPhase;
            }
        }
        #endregion

        #region Methods
        private void AllSelectedChanged()
        {
            if (allVisibleChanging) return;

            try
            {
                allVisibleChanging = true;
                if (AllVisible == true)
                {
                    foreach (PhaseItemViewModel phaseItemViewModel in PhaseItemsViewModelList)
                        phaseItemViewModel.Visible = true;
                }
                else if (AllVisible == false)
                {
                    foreach (PhaseItemViewModel phaseItemViewModel in PhaseItemsViewModelList)
                        phaseItemViewModel.Visible = false;
                }
            }
            finally
            {
                allVisibleChanging = false;
            }
        }

        private void RecheckAllSelected()
        {
            if (allVisibleChanging) return;

            try
            {
                allVisibleChanging = true;

                if (PhaseItemsViewModelList.All(p => p.Visible))
                    AllVisible = true;
                else if (PhaseItemsViewModelList.All(p => !p.Visible))
                    AllVisible = false;
                else
                    AllVisible = null;
            }
            finally
            {
                allVisibleChanging = false;
            }
        }

        private void phaseItemListSynchronization(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PhaseItem addedPhaseItem = (PhaseItem)e.NewItems[0];
                    if (addedPhaseItem != null)
                    {
                        PhaseItemsViewModelList.Add(new PhaseItemViewModel(addedPhaseItem));
                        PhaseItemsViewModelList.Last().PropertyChanged += VisibleOnPropertyChanged;
                    }
                    break;
                //case NotifyCollectionChangedAction.Remove:
                //    PhaseItem removedPhaseItem = (PhaseItem)e.OldItems[0];
                //    if (removedPhaseItem != null)
                //        _PhaseItemsList.Remove(new PhaseItemViewModel(removedPhaseItem));
                //    break;
            }
        }

        private void VisibleOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(PhaseItemViewModel.Visible))
                RecheckAllSelected();
        }
        #endregion

    }
}
