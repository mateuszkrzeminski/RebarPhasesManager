using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;


namespace RebarPhasesManager.Model
{
    public class TeklaConnector
    {
        #region Members

        #endregion

        #region Properties

        #endregion


        #region Construction
        public TeklaConnector()
        {
            Tekla.Structures.Model.Model myModel = new Tekla.Structures.Model.Model();

            initializePhaseViewList();
            RegisterEventHandler();
        }
        #endregion

        #region Methods

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

        private void createPhaseViewList(RebarsSelector reinforcementList)
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

        private void createReinforcementList()
        {
            foreach (ModelObject modelObject in modelObjectsEnumerator)
            {
                if (modelObject is Reinforcement)
                {
                    reinforcements.Add((Reinforcement)modelObject);
                }
            }
        }








        // Tekla Events handling

        Events _events = new Events();
        private object _selectionEventHandlerLock = new object();
        private object _changedObjectHandlerLock = new object();

        public void RegisterEventHandler()
        {
            _events.SelectionChange += Events_SelectionChangeEvent;
            _events.ModelObjectChanged += Events_ModelObjectChangedEvent;
            _events.Register();
        }

        public void UnRegisterEventHandler()
        {
            _events.UnRegister();
        }

        void Events_SelectionChangeEvent()
        {
            /* Make sure that the inner code block is running synchronously */
            lock (_selectionEventHandlerLock)
            {
                //getSelectedModelObjectsEnumerator();
            }
        }

        void Events_ModelObjectChangedEvent(List<ChangeData> changes)
        {
            /* Make sure that the inner code block is running synchronously */
            lock (_changedObjectHandlerLock)
            {
                ModelObjectVisualization.ClearAllTemporaryStates();
                phaseViewList = null;
                OnPropertyChanged("PhaseViewList");
                createReinforcementList();
                createPhaseViewList(reinforcements);
                OnPropertyChanged("PhaseViewList");
                SetTemporaryColor();
            }
        }

    }
}
