using System;
using System.Collections;
using System.Collections.Generic;
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
            RebarsList = rebarsList;
        }
        #endregion

        #region Properties
        public Phase Phase { get; private set; }
        public bool Visible { get; set; } = true;
        public Color Color { get; private set; }
        public List<Reinforcement> RebarsList { get; private set; }
        //public int CountRebars
        //{
        //    get
        //    {
        //        return RebarsList.Count;
        //    }
        //}
        #endregion

        #region Methods
        public void AddRebar(Reinforcement rebar)
        {
            RebarsList.Add(rebar);
        }

        public bool RemoveRebar(Reinforcement rebar)
        {
            if (RebarsList.RemoveAll(r => r.Identifier.ID == rebar.Identifier.ID) > 0)
                return true;
            else return false;
        }

        public bool ContainsRebar(Reinforcement rebar)
        {
            foreach (Reinforcement rf in RebarsList)
            {
                if (rf.Equals(rebar))
                    return true;
            }
            return false;
        }


        #endregion




    }
}
