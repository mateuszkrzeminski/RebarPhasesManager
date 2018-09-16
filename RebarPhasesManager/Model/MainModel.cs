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
    class MainModel
    {
        #region Members
        private RebarsSelector rebarsSelector;
        #endregion

        #region Properties
        public List<PhaseItem> PhaseItemList { get; private set; }
        public PhaseItem SelectedPhaseItem { get; set; }
        public ArrayList SelectedRebars
        {
            get
            {
                return new ArrayList(rebarsSelector.Cast<object>().ToArray());
            }
        }
        #endregion
        public MainModel()
        {
            rebarsSelector = new RebarsSelector();
        }
        #region Construction

        #endregion

        #region Methods

        public void AddRebars()
        {

        }

        public void RemoveRebars()
        {

        }

        public void ModifyPhase()
        {

        }

        public void SelectByPhase()
        {
            rebarsSelector.SelectRebars(SelectedPhaseItem.RebarsList);
        }

        private void createPhaseViewList(List<Reinforcement> rebarsList)
        {

            var queryReinforcemenByPhase =
                (from rf in rebarsList
                 let phase = rf.WhatIsMyPhase()
                 group rf by new { number = phase.PhaseNumber, name = phase.PhaseName, comment = phase.WhatIsMyUserComment(PhaseCodes), current = phase.IsCurrentPhase }
                ).OrderBy(o => o.Key.number).ToList();

            phaseViewList = new List<PhaseView>();

            foreach (var phaseGroup in queryReinforcemenByPhase)
            {
                List<Reinforcement> reinforcementlist = new List<Reinforcement>();
                foreach (Reinforcement rf in phaseGroup)
                    reinforcementlist.Add(rf);
                int index = queryReinforcemenByPhase.IndexOf(phaseGroup);
                phaseViewList.Add(new PhaseView(true, hexColors[index], teklaColors[index], phaseGroup.Key.number, phaseGroup.Key.name, phaseGroup.Key.comment, phaseGroup.Key.current, reinforcementlist));
            }
        }

        #endregion






        private void initializePhaseViewList()
        {
            modelObjectsEnumerator = getSelectedModelObjectsEnumerator();
            createReinforcementList();
            createPhaseViewList(reinforcements);

            SetTemporaryColor();
        }

        public void ReinitializePhaseViewList()
        {
            ModelObjectVisualization.ClearAllTemporaryStates();
            phaseViewList = null;
            OnPropertyChanged("PhaseViewList");
            getModelObjectsEnumerator();
            createReinforcementList();
            createPhaseViewList(reinforcements);
            OnPropertyChanged("PhaseViewList");

            SetTemporaryColor();
        }

        private void DUPAcreatePhaseViewList(List<Reinforcement> reinforcementList)
        {
            phaseViewList = null;

            var queryReinforcemenByPhase =
                (from rf in reinforcementList
                 let phase = rf.WhatIsMyPhase()
                 group rf by new { number = phase.PhaseNumber, name = phase.PhaseName, comment = phase.WhatIsMyUserComment(PhaseCodes), current = phase.IsCurrentPhase }
                ).OrderBy(o => o.Key.number).ToList();

            phaseViewList = new List<PhaseView>();

            foreach (var phaseGroup in queryReinforcemenByPhase)
            {
                List<Reinforcement> reinforcementlist = new List<Reinforcement>();
                foreach (Reinforcement rf in phaseGroup)
                    reinforcementlist.Add(rf);
                int index = queryReinforcemenByPhase.IndexOf(phaseGroup);
                phaseViewList.Add(new PhaseView(true, hexColors[index], teklaColors[index], phaseGroup.Key.number, phaseGroup.Key.name, phaseGroup.Key.comment, phaseGroup.Key.current, reinforcementlist));
            }
        }





    }
}
