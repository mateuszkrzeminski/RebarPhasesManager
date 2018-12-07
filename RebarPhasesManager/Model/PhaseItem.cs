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

namespace RebarPhaseManager.Model
{
    class PhaseItem : ObservedObject
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
        private bool selected = false;
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
                    setTempColor();
                }
            }
        }

        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnSelectedChanged();
                }
            }
        }

        public Color Color { get; private set; }
        public List<Reinforcement> RebarList { get; private set; }
        #endregion
        
        #region Methods
        public void AddRebar(Reinforcement rebar)
        {
            RebarList.Add(rebar);
            OnNoOfRebarsChanged();
            setTempColor(rebar);
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

        private void setTempColor()
        {
            if (visible == true)
                RebarVisualizator.SetTempColor(RebarList, Color);
            else
                RebarVisualizator.SetTempColor(RebarList, Data.InvisibleColor);
        }

        private void setTempColor(Reinforcement rebar)
        {
            if (visible == true)
                RebarVisualizator.SetTempColor(rebar, Color);
            else
                RebarVisualizator.SetTempColor(rebar, Data.InvisibleColor);
        }

        #endregion

        #region Events
        public event EventHandler NoOfRebarsChanged;
        protected virtual void OnNoOfRebarsChanged()
        {
            if (NoOfRebarsChanged != null)
                NoOfRebarsChanged(this, EventArgs.Empty);
        }

        public event EventHandler SelectedChanged;
        protected virtual void OnSelectedChanged()
        {
            if (NoOfRebarsChanged != null)
                SelectedChanged(this, EventArgs.Empty);
        }
        #endregion




    }
}
