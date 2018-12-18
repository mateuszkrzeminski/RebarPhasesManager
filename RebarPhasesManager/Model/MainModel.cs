using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public PhaseCollection phaseCollection;
        #endregion

        #region Properties
        public ObservableCollection<PhaseItem> PhaseItemsList { get; private set; } = new ObservableCollection<PhaseItem>();
        #endregion

        #region Construction
        public MainModel()
        {
            myModel = new Tekla.Structures.Model.Model();
            phaseCollection = myModel.GetPhases();
            PhaseItemsList.CollectionChanged += PhaseItemsList_CollectionChanged;
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
                    addPhaseItem(phase, new List<Reinforcement>() { rebar });
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

        public void SelectByPhase()
        {
            ArrayList rebarsToSelect = new ArrayList();
            foreach (PhaseItem phaseItem in PhaseItemsList)
            {
                if (phaseItem.Selected)
                    rebarsToSelect.AddRange(phaseItem.RebarList);
            }
            if (rebarsToSelect.Count > 0)
                rebarsSelector.SelectRebars(rebarsToSelect);
        }

        public void ModifyPhase()
        {
            PhaseItem phaseItemToSet = PhaseItemsList.First(ph => ph.Selected);
            foreach (Reinforcement rebar in rebarsSelector)
            {
                Phase phase = rebar.WhatIsMyPhase();
                if (rebar.SetPhase(phaseItemToSet.Phase))
                {
                    RemoveRebar(rebar, phase);
                    AddRebar(rebar, rebar.WhatIsMyPhase());
                }
            }
        }

        public void AddPhaseItems(IList phaseList)
        {
            foreach (var element in phaseList)
            {
                Phase phase = element as Phase;
                if (!PhaseItemsList.Any(ph => ph.Phase.PhaseNumber == phase.PhaseNumber))
                {
                    addPhaseItem(phase);
                }
            }
        }

        private void addPhaseItem(Phase phase)
        {
            PhaseItemsList.Add(new PhaseItem(phase, colors.Dequeue(), new List<Reinforcement>()));
        }

        private void addPhaseItem(Phase phase, List<Reinforcement> rebarList)
        {
            PhaseItemsList.Add(new PhaseItem(phase, colors.Dequeue(), rebarList));
        }

        public void RemovePhaseItems()
        {
            List<PhaseItem> phaseItemsToRemove = (from ph in PhaseItemsList where ph.Selected == true select ph).ToList<PhaseItem>();
            foreach (PhaseItem phaseToRemove in phaseItemsToRemove)
            {
                RemovePhaseItem(phaseToRemove);
            }
        }

        public void RemovePhaseItem(PhaseItem phaseItemToRemove)
        {
            if (phaseItemToRemove.RebarList.Count > 0)
                RebarVisualizator.SetTempColor(phaseItemToRemove.RebarList, Data.NotAnalyzedColor);
            colors.Enqueue(phaseItemToRemove.Color);
            PhaseItemsList.Remove(phaseItemToRemove);
        }


        public void SelectByRebars()
        {
            foreach (PhaseItem phaseItem in PhaseItemsList)
            {
                phaseItem.Selected = false;
            }
            foreach (Reinforcement rebar in rebarsSelector)
            {
                foreach (PhaseItem phaseItem in PhaseItemsList)
                {
                    if (rebar.WhatIsMyPhase().PhaseNumber == phaseItem.Phase.PhaseNumber)
                        if (phaseItem.ContainsRebar(rebar))
                        {
                            phaseItem.Selected = true;
                            break;
                        }  
                }
            }
        }

        private void createPhaseItemList()
        {
            IEnumerable<Reinforcement> selectedRebars = rebarsSelector.Cast<Reinforcement>();

            List<IGrouping<int, Reinforcement>> rebarsByPhase =
                (from rebar in selectedRebars
                 let phase = rebar.WhatIsMyPhase()
                 group rebar by phase.PhaseNumber).ToList();

            ModelObjectVisualization.SetTemporaryStateForAll(Data.NotAnalyzedColor);

            foreach (IGrouping<int, Reinforcement> phaseGroup in rebarsByPhase)
            {
                addPhaseItem(phaseGroup.First().WhatIsMyPhase(), phaseGroup.ToList<Reinforcement>());
            }
        }

        private void PhaseItemsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PhaseItem addedPhaseItem = (PhaseItem)e.NewItems[0];
                    if (addedPhaseItem != null)
                        addedPhaseItem.NoOfRebarsChanged += NoOfRebarsChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    PhaseItem removedPhaseItem = (PhaseItem)e.OldItems[0];
                    if (removedPhaseItem != null)
                        removedPhaseItem.NoOfRebarsChanged -= NoOfRebarsChanged;
                    break;
            }
        }

        private void NoOfRebarsChanged(object sender, EventArgs e)
        {
            PhaseItem phaseItem = (PhaseItem)sender;
            if (phaseItem.RebarList.Count == 0)
            {
                RemovePhaseItem(phaseItem);
            }
        }
        #endregion
    }
}
