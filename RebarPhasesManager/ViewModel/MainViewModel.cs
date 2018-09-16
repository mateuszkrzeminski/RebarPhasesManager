using System;
using System.Collections.Generic;
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

        }
        #endregion

        #region Members
        private List<PhaseItemViewModel> _phaseItems = new List<PhaseItemViewModel>();
        #endregion

        #region Properties
        public List<PhaseItemViewModel> PhaseItems
        {
            get { return _phaseItems; }
            set { _phaseItems = value; }
        }
        #endregion

        #region Commands

        #endregion

    }
}
