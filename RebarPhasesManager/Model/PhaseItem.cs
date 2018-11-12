using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public PhaseItem(Phase phase, Color teklaColor, List<Reinforcement> rebarsList)
        {
            Phase = phase;
            Color = teklaColor;
            RebarList = rebarsList;
            RebarVisualizator.SetTempColor(RebarList, Color);
        }
        #endregion

        #region Members
        private bool visible = true;
        #endregion

        #region Properties
        public Phase Phase { get; private set; }

        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                        if (value == true)
                        RebarVisualizator.SetTempColor(RebarList, Color);
                    else
                        RebarVisualizator.SetTempColor(RebarList, Data.InvisibleColor);
                }
            }
        }
        public Color Color { get; private set; }
        public List<Reinforcement> RebarList { get; private set; }
        public int CountRebars { get { return RebarList.Count; } }
        #endregion
        
        #region Methods
        public void AddRebar(Reinforcement rebar)
        {
            RebarList.Add(rebar);
            OnNoOfRebarsChanged();
            RebarVisualizator.SetTempColor(rebar, Color);
        }

        public bool RemoveRebar(Reinforcement rebar)
        {
            if (RebarList.RemoveAll(r => r.Identifier.ID == rebar.Identifier.ID) > 0)
            {
                OnNoOfRebarsChanged();
                RebarVisualizator.SetTempColor(rebar, Data.NotAnalyzedColor);
                return true;
            }
            else return false;
        }

        public bool ContainsRebar(Reinforcement rebar)
        {
            foreach (Reinforcement rf in RebarList)
            {
                if (rf.Equals(rebar))
                    return true;
            }
            return false;
        }

        #endregion

        #region Events
        public event EventHandler NoOfRebarsChanged;
        protected virtual void OnNoOfRebarsChanged()
        {
            if (NoOfRebarsChanged != null)
                NoOfRebarsChanged(this, EventArgs.Empty);
        }
        #endregion




    }
}
