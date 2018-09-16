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
        public PhaseItemViewModel(bool visible, string hexColor, Color teklaColor, int number, string name, string comment, int current, List<Reinforcement> reinforcementObjectsList)
        {
            _phaseItem = new PhaseItem(visible, hexColor, teklaColor, number, name, comment, current, reinforcementObjectsList);
        }
        #endregion

        #region Members
        private PhaseItem _phaseItem;
        #endregion

        #region Properties
        public PhaseItem PhaseItem
        {
            get { return _phaseItem; }
            set { _phaseItem = value; }
        }

        public bool Visible
        {
            get { return PhaseItem.Visible; }
            set
            {
                if (PhaseItem.Visible != value)
                {
                    PhaseItem.Visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        public string HexColor
        {
            get { return PhaseItem.HexColor; }
            set
            {
                if (PhaseItem.HexColor != value)
                {
                    PhaseItem.HexColor = value;
                    OnPropertyChanged("HexColor");
                }
            }
        }

        public Color TeklaColor
        {
            get { return PhaseItem.TeklaColor; }
            set
            {
                if (PhaseItem.TeklaColor != value)
                {
                    PhaseItem.TeklaColor = value;
                    OnPropertyChanged("TeklaColor");
                }
            }
        }

        public int Number
        {
            get { return PhaseItem.Number; }
            set
            {
                if (PhaseItem.Number != value)
                {
                    PhaseItem.Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public string Name
        {
            get { return PhaseItem.Name; }
            set
            {
                if (PhaseItem.Name != value)
                {
                    PhaseItem.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Comment
        {
            get { return PhaseItem.Comment; }
            set
            {
                if (PhaseItem.Comment != value)
                {
                    PhaseItem.Comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public int Current
        {
            get { return PhaseItem.Current; }
            set
            {
                if (PhaseItem.Current != value)
                {
                    PhaseItem.Current = value;
                    OnPropertyChanged("Current");
                }
            }
        }

        public List<Reinforcement> ReinforcementObjectsList
        {
            get { return PhaseItem.RebarsList; }
            set
            {
                if (PhaseItem.RebarsList != value)
                {
                    PhaseItem.ReinforcementObjectsList = value;
                    OnPropertyChanged("ReinforcementObjectsList");
                }
            }
        }
        #endregion
    }
}
