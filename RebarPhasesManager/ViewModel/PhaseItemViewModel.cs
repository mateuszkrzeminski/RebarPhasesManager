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
        public PhaseItemViewModel(bool visible, string hexColor, int number, string name, string comment, int current)
        {
            
        }
        #endregion

        #region Members
        private PhaseItem _phaseItem;
        #endregion

        //#region Properties
        //public PhaseItem PhaseItem
        //{
        //    get { return _phaseItem; }
        //    set { _phaseItem = value; }
        //}

        //public bool Visible
        //{
        //    get { return PhaseItem.Visible; }
        //    set
        //    {
        //        if (PhaseItem.Visible != value)
        //        {
        //            PhaseItem.Visible = value;
        //            OnPropertyChanged("Visible");
        //        }
        //    }
        //}




        //public int Number
        //{
        //    get { return PhaseItem.Number; }
        //    set
        //    {
        //        if (PhaseItem.Number != value)
        //        {
        //            PhaseItem.Number = value;
        //            OnPropertyChanged("Number");
        //        }
        //    }
        //}

        //public string Name
        //{
        //    get { return PhaseItem.Name; }
        //    set
        //    {
        //        if (PhaseItem.Name != value)
        //        {
        //            PhaseItem.Name = value;
        //            OnPropertyChanged("Name");
        //        }
        //    }
        //}

        //public string Comment
        //{
        //    get { return PhaseItem.Comment; }
        //    set
        //    {
        //        if (PhaseItem.Comment != value)
        //        {
        //            PhaseItem.Comment = value;
        //            OnPropertyChanged("Comment");
        //        }
        //    }
        //}

        //public int Current
        //{
        //    get { return PhaseItem.Current; }
        //    set
        //    {
        //        if (PhaseItem.Current != value)
        //        {
        //            PhaseItem.Current = value;
        //            OnPropertyChanged("Current");
        //        }
        //    }
        //}
        //#endregion
    }
}
