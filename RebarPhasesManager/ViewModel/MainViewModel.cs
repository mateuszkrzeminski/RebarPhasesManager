using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RebarPhaseManager.Model;


namespace RebarPhaseManager.ViewModel
{
    class MainViewModel : ObservedObject
    {
        #region Construction
        public MainViewModel()
        {
            _mainModel.PhaseItemsList.CollectionChanged += phaseItemListSynchronization;
            PhaseItemsViewModelList.CollectionChanged += PhaseItemsViewModelList_CollectionChanged;
            RecheckAllSelected();
        }
        #endregion

        #region Members
        private MainModel _mainModel = new MainModel();
        private bool? allVisible;
        private bool allVisibleChanging;
        #endregion

        #region Properties
        public ObservableCollection<PhaseItemViewModel> PhaseItemsViewModelList { get; } = new ObservableCollection<PhaseItemViewModel>();
        public IList SelectedPhaseItems { get; set; }

        public IEnumerable PhaseCollection { get { return _mainModel.phaseCollection; } }
        public IList SelectedFromPhaseCollection { get; set; }

        public bool? AllVisible
        {
            get { return allVisible; }
            set
            {
                if (allVisible != value)
                {
                    allVisible = value;
                    AllSelectedChanged();
                    OnPropertyChanged("AllVisible");
                }
            }
        }

        public bool InvertAllChbx
        {
            get
            {
                if (PhaseItemsViewModelList.Count(p => p.Visible) <= PhaseItemsViewModelList.Count(p => !p.Visible))
                    return true;
                else return false;
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
                    removeRebars = new RelayCommand(o => _mainModel.RemoveRebars(), o => PhaseItemsViewModelList.Count > 0);
                return removeRebars;
            }
        }

        private ICommand selectByPhase;
        public ICommand SelectByPhase
        {
            get
            {
                if (selectByPhase == null)
                    selectByPhase = new RelayCommand(o => _mainModel.SelectByPhase(), o => SelectedPhaseItems != null);
                return selectByPhase;
            }
        }
        private ICommand modifyPhase;
        public ICommand ModifyPhase
        {
            get
            {
                if (modifyPhase == null)
                    modifyPhase = new RelayCommand(o => _mainModel.ModifyPhase(), o => (SelectedPhaseItems != null && SelectedPhaseItems.Count == 1));
                return modifyPhase;
            }
        }

        private ICommand addPhaseItems;
        public ICommand AddPhaseItems
        {
            get
            {
                if (addPhaseItems == null)
                    addPhaseItems = new RelayCommand(o => _mainModel.AddPhaseItems(SelectedFromPhaseCollection), o => SelectedFromPhaseCollection != null);
                return addPhaseItems;
            }
        }

        private ICommand removePhaseItem;
        public ICommand RemovePhaseItem
        {
            get
            {
                if (removePhaseItem == null)
                    removePhaseItem = new RelayCommand(o => _mainModel.RemovePhaseItems(), o => SelectedPhaseItems != null);
                return removePhaseItem;
            }
        }
        private ICommand selectByRebars;
        public ICommand SelectByRebars
        {
            get
            {
                if (selectByRebars == null)
                    selectByRebars = new RelayCommand(o => _mainModel.SelectByRebars(), o => PhaseItemsViewModelList.Count > 0);
                return selectByRebars;
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
                {
                    AllVisible = null;
                    OnPropertyChanged("InvertAllChbx");
                }
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
                        PhaseItemsViewModelList.Add(new PhaseItemViewModel(addedPhaseItem));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PhaseItem removedPhaseItem = (PhaseItem)e.OldItems[0];
                    if (removedPhaseItem != null)
                        PhaseItemsViewModelList.RemoveAll(p => p.PhaseItem == removedPhaseItem);
                    break;
            }
        }

        private void PhaseItemsViewModelList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PhaseItemViewModel addedPhaseItem = (PhaseItemViewModel)e.NewItems[0];
                    if (addedPhaseItem != null)
                        addedPhaseItem.PropertyChanged += VisibleOnPropertyChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PhaseItemViewModel removedPhaseItem = (PhaseItemViewModel)e.OldItems[0];
                    if (removedPhaseItem != null)
                        removedPhaseItem.PropertyChanged -= VisibleOnPropertyChanged;
                    break;
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
