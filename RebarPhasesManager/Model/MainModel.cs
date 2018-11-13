using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhaseManager.Model
{
    partial class MainModel
    {
        #region Members
        private RebarsSelector rebarsSelector = new RebarsSelector();
        private Queue<Color> colors = new Queue<Color>(Data.TeklaColors);
        private Tekla.Structures.Model.Model myModel;
        #endregion

        #region Properties
        public ObservableCollection<PhaseItem> PhaseItemsList { get; private set; } = new ObservableCollection<PhaseItem>();
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
            if (PhaseItemsList.Count != 0)
            {
                foreach (Reinforcement rebar in rebarsSelector)
                {
                    AddRebar(rebar, rebar.WhatIsMyPhase());
                }
            }
            else
            {
                createPhaseItemList();
            }
        }

        public void AddRebar(Reinforcement rebar, Phase phase)
        {
            int index = PhaseItemsList.Count();
            for (int i = 0; i < index; i++)
            {
                if (phase.PhaseNumber == PhaseItemsList[i].Phase.PhaseNumber)
                {
                    if (PhaseItemsList[i].ContainsRebar(rebar))
                        break;
                    else
                    {
                        PhaseItemsList[i].AddRebar(rebar);
                        break;
                    }
                }
                else
                    if (i == index - 1)
                    AddPhaseItem(phase, new List<Reinforcement>() { rebar });
            }
        }

        public void RemoveRebars()
        {
            foreach (Reinforcement rebar in rebarsSelector)
            {
                RemoveRebar(rebar, rebar.WhatIsMyPhase());
            }
        }

        public void RemoveRebar(Reinforcement rebar, Phase phase)
        {
            int index = PhaseItemsList.Count();
            for (int i = 0; i < index; i++)
            {
                if (phase.PhaseNumber == PhaseItemsList[i].Phase.PhaseNumber)
                    if (PhaseItemsList[i].RemoveRebar(rebar))
                        break;
            }
        }

        public void SelectByPhase(PhaseItem selectedPhaseItem)
        {
            rebarsSelector.SelectRebars(new ArrayList(selectedPhaseItem.RebarList));
        }

        public void ModifyPhase(PhaseItem selectedPhaseItem)
        {
            foreach (Reinforcement rebar in rebarsSelector)
            {
                Phase phase = rebar.WhatIsMyPhase();
                if (rebar.SetPhase(selectedPhaseItem.Phase))
                {
                    RemoveRebar(rebar, phase);
                    AddRebar(rebar, rebar.WhatIsMyPhase());
                }
            }
        }
 
        public void AddPhaseItem(Phase phase, List<Reinforcement> rebarList)
        {
            PhaseItemsList.Add(new PhaseItem(phase, colors.Dequeue(), rebarList));
        }
        
        private void createPhaseItemList()
        {
            IEnumerable<Reinforcement> selectedRebars = rebarsSelector.Cast<Reinforcement>();

            List<IGrouping<int, Reinforcement>> rebarsByPhase =
                (from rebar in selectedRebars
                 let phase = rebar.WhatIsMyPhase()
                 group rebar by phase.PhaseNumber)
                 .OrderBy(o => o.Key).ToList();

            ModelObjectVisualization.SetTemporaryStateForAll(Data.NotAnalyzedColor);

            foreach (var phaseGroup in rebarsByPhase)
            {
                AddPhaseItem(phaseGroup.First().WhatIsMyPhase(), phaseGroup.ToList<Reinforcement>());
            }
        }

        #endregion
    }
}
