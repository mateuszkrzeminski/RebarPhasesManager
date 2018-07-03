using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;


namespace RebarPhasesManager
{
    partial class PhaseManager : INotifyPropertyChanged
    {
        Model myModel;

        private List<PhaseView> phaseViewList = null;
        public List<PhaseView> PhaseViewList { get { return phaseViewList; } }

        private ModelObjectEnumerator modelObjectsEnumerator;

        private List<Reinforcement> reinforcements = new List<Reinforcement>();

        public PhaseView SelectedPhaseView { private get; set; }

        public PhaseManager()
        {
            myModel = new Model();
            ModelObjectEnumerator.AutoFetch = true;
            initializePhaseViewList();
            RegisterEventHandler();
        }

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

        private void createPhaseViewList(List<Reinforcement> reinforcementList)
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

        private void getModelObjectsEnumerator()
        {
            ModelObjectEnumerator selectedModelObjetsEnumerator = getSelectedModelObjectsEnumerator();

            if (selectedModelObjetsEnumerator.GetSize() > 0)
            {
                modelObjectsEnumerator = selectedModelObjetsEnumerator;
            }
            else
            {
                    try
                    {
                        modelObjectsEnumerator = pickReinforcements();
                    }
                    catch (Exception)
                    {
                         throw;
                    }
                }
         }
  
        private ModelObjectEnumerator getSelectedModelObjectsEnumerator()
        {
            Tekla.Structures.Model.UI.ModelObjectSelector selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            return selector.GetSelectedObjects();
        }

        private ModelObjectEnumerator pickReinforcements()
        {
            Picker picker = new Picker();
            try
            {
                return picker.PickObjects(Picker.PickObjectsEnum.PICK_N_REINFORCEMENTS);
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        //public void ModifyPhaseViewList()
        //{

        //}
        
        //private void modifyPhase()
        //{
        //    ModelObjectEnumerator objects = proceedWithModelObjects();
        //    Phase phase = new Phase(SelectedPhaseView.Number);
        //    foreach (var rf in objects)
        //    {
        //        if
        //        rf.SetPhase(phase);
        //    }
        //    OnPropertyChanged("PhaseViewList");
        //    updatePhaseViewList();
        //    SetTemporaryColor();
        //}



        //private void updatePhaseViewList(List<ChangeData> changes)
        //{
        //    foreach (ChangeData changeData in changes)
        //    {
        //        changeData.Object.
        //    }

        //    foreach (PhaseView phaseView in PhaseViewList)
        //    {
        //        phaseView.ReinforcementObjectsList = null;
        //    }

        //    foreach (ModelObject modelObject in selectedModelObjectsEnumerator)
        //    {
        //        modelObject.WhatIsMyPhase();
        //    }


        //    List<Reinforcement> ReinforcementList = createReinforcementList();



        //    var queryReinforcemenByPhase =
        //        (from rf in ReinforcementList
        //         let phase = rf.WhatIsMyPhase()
        //         group rf by new { number = phase.PhaseNumber, name = phase.PhaseName, comment = phase.PhaseComment, current = phase.IsCurrentPhase }
        //        ).ToList();

        //    phaseViewList = new List<PhaseView>();

        //    foreach (var phaseGroup in queryReinforcemenByPhase)
        //    {
        //        List<Reinforcement> reinforcementlist = new List<Reinforcement>();
        //        foreach (Reinforcement rf in phaseGroup)
        //            reinforcementlist.Add(rf);
        //        int index = queryReinforcemenByPhase.IndexOf(phaseGroup);
        //        phaseViewList.Add(new PhaseView(true, hexColors[index], teklaColors[index], phaseGroup.Key.number, phaseGroup.Key.name, phaseGroup.Key.comment, phaseGroup.Key.current, reinforcementlist));
        //    }

        //    phaseViewList.OrderBy(PhaseView => PhaseView.Number);
        //}

        public void SetTemporaryColor()
        {
            foreach (PhaseView phaseView in phaseViewList)
            {
                if (phaseView.Visible)
                {
                    List<ModelObject> modelObjects = new List<ModelObject>();
                    foreach (Reinforcement rf in phaseView.ReinforcementObjectsList)
                    {
                        modelObjects.Add(rf);
                    }
                    ModelObjectVisualization.SetTemporaryState(modelObjects, phaseView.TeklaColor);
                }
                else
                {
                    List<ModelObject> modelObjects = new List<ModelObject>();
                    foreach (Reinforcement rf in phaseView.ReinforcementObjectsList)
                    {
                        modelObjects.Add(rf);
                    }
                    ModelObjectVisualization.SetTemporaryState(modelObjects, new Color(0.5, 0.5, 0.5));
                }
            }
        }






        // Events handling
       
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

                        
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
