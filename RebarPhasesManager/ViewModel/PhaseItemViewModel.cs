using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

using RebarPhasesManager.Model;

namespace RebarPhasesManager.ViewModel
{
    class PhaseItemViewModel : ObservedObject
    {
        #region Construction
        public PhaseItemViewModel(PhaseItem phaseItem)
        {
            _phaseItem = phaseItem;
        }
        #endregion

        #region Members
        private PhaseItem _phaseItem;
        #endregion

        #region Properties
        
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
        public string Comment { get { return _phaseItem.Phase.PhaseComment; } }
        public int Current { get { return _phaseItem.Phase.IsCurrentPhase; } }
        #endregion
    }
}
