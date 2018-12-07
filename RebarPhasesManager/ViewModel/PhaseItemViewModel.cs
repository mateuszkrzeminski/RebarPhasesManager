using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

using RebarPhaseManager.Model;

namespace RebarPhaseManager.ViewModel
{
    class PhaseItemViewModel : ObservedObject
    {
        #region Construction
        public PhaseItemViewModel(PhaseItem phaseItem)
        {
            _phaseItem = phaseItem;
            _phaseItem.NoOfRebarsChanged += _phaseItem_NoOfRebarsChanged;
            _phaseItem.SelectedChanged += _phaseItem_SelectedChanged;
        }

        #endregion

        #region Members
        private PhaseItem _phaseItem;
        #endregion

        #region Properties

        public PhaseItem PhaseItem { get { return _phaseItem; } }

        public bool Selected
        {
            get { return _phaseItem.Selected; }
            set
            {
                if (_phaseItem.Selected != value)
                {
                    _phaseItem.Selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }
        public bool Visible
        {
            get { return _phaseItem.Visible; }
            set
            {
                if (_phaseItem.Visible != value)
                {
                    _phaseItem.Visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        public Color Color { get { return _phaseItem.Color; } }
        public int Number { get { return _phaseItem.Phase.PhaseNumber; } }
        public string Name { get { return _phaseItem.Phase.PhaseName; } }
        public string Comment { get { return _phaseItem.Phase.WhatIsMyUserComment(); } }
        public int Current { get { return _phaseItem.Phase.IsCurrentPhase; } }
        public int CountRebars { get { return _phaseItem.RebarList.Count(); } }
        #endregion

        private void _phaseItem_NoOfRebarsChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("CountRebars");
        }

        private void _phaseItem_SelectedChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Selected");
        }
    }
}
