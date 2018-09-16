using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhasesManager.Model
{
    class PhaseItem
    {
        #region Construction
        public PhaseItem(Phase phase, bool visible, string hexColor, Color teklaColor, int number, string name, string comment, int current, List<Reinforcement> rebarsList)
        {
            Phase = phase;
            Visible = visible;
            HexColor = hexColor;
            TeklaColor = teklaColor;
            Number = number;
            Name = name;
            Comment = comment;
            Current = current;
            RebarsList = rebarsList;
        }
        #endregion

        #region Properties
        public Phase Phase { get; private set; }
        public bool Visible { get; set; }
        public string HexColor { get; private set; }
        public Color TeklaColor { get; private set; }
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public int Current { get; private set; }
        public ArrayList RebarsList { get; private set; }
        public int CountRebars
        {
            get
            {
                return RebarsList.Count;
            }
        }
        #endregion

        #region Methods
        public void AddRebar(Reinforcement rf)
        {
            RebarsList.Add(rf);
        }

        public void RemoveRebar(Reinforcement rf)
        {
            RebarsList.Remove(rf);
        }


        #endregion




    }
}
