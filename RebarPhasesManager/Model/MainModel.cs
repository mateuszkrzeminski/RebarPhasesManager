using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhasesManager.Model
{
    partial class MainModel
    {
        #region Members
        private RebarsSelector rebarsSelector = new RebarsSelector();
        private Queue<Color> colors = new Queue<Color>(teklaColors);

        Tekla.Structures.Model.Model myModel;
        #endregion

        #region Properties
        public List<PhaseItem> PhaseItemList { get; private set; } = new List<PhaseItem>();
        public PhaseItem SelectedPhaseItem { get; set; }
         #endregion

        #region Construction
        public MainModel()
        {
            myModel = new Tekla.Structures.Model.Model();
        }
        #endregion

        #region Methods
        public void AddRebars()
        {
            if (PhaseItemList.Count != 0)
            {
                foreach (Reinforcement rebar in rebarsSelector)
                {
                    Phase phase = rebar.WhatIsMyPhase();
                    foreach (PhaseItem phaseItem in PhaseItemList)
                    {
                        if (phase == phaseItem.Phase)
                            phaseItem.AddRebar(rebar);
                        else
                        {
                            AddPhaseItem(phase, new List<Reinforcement>() { rebar });
                        }
                    }
                }
            }
            else
            {
                createPhaseItemList();
            }
        }

        public void RemoveRebars()
        {
            foreach (Reinforcement rebar in rebarsSelector)
            {
                Phase phase = rebar.WhatIsMyPhase();
                foreach (PhaseItem phaseItem in PhaseItemList)
                {
                    if (phase == phaseItem.Phase)
                        phaseItem.RemoveRebar(rebar);
                }
            }
        }

        public void ModifyPhase()
        {

        }

        public void SelectByPhase()
        {
            rebarsSelector.SelectRebars(new ArrayList(SelectedPhaseItem.RebarsList));
        }

        public void AddPhaseItem(Phase phase, List<Reinforcement> rebarList)
        {
            PhaseItemList.Add(new PhaseItem(phase, colors.Dequeue(), rebarList));
        }


        private void createPhaseItemList()
        {
            IEnumerable<Reinforcement> selectedRebars = rebarsSelector.Cast<Reinforcement>();

            IEnumerable<IGrouping<int, Reinforcement>> rebarsByPhase =
                (from rebar in selectedRebars
                 let phase = rebar.WhatIsMyPhase()
                 group rebar by phase.PhaseNumber);

            foreach (var phaseGroup in rebarsByPhase)
            {
                AddPhaseItem(phaseGroup.First().WhatIsMyPhase(), phaseGroup.ToList<Reinforcement>());
            }
        }

        #endregion
    }
}
