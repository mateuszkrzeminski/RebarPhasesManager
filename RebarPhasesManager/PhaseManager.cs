using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;


namespace RebarPhasesManager
{
    class PhaseManager : INotifyPropertyChanged
    {
        Model myModel;
        private List<PhaseView> phaseViewList;
        public List<PhaseView> PhaseViewList { get { return phaseViewList; } }

        string[] hexColors = { "#ff0000", "#ffff00", "#00ff00", "#00ffff", "#4040ff", "#ff40ff", "#bf0000", "#bfbf00", "#00bf00", "#00bfbf", "#3030bf", "#bf30bf", "#7f0000", "#7f8000", "#008000", "#008080", "#202080", "#802080", "#ff4040", "#ffff40", "#40ff40", "#80ffff", "#8080ff", "#ff80ff", "#bf3030", "#bfbf30", "#30bf30", "#60bfbf", "#6060bf", "#bf60bf", "#7f2020", "#7f8020", "#208020", "#408080", "#404080", "#804080", "#ff8080", "#ffff80", "#7fff80", "#00aaff", "#5500ff", "#ff00aa", "#bf6060", "#bfbf60", "#60bf60", "#0080bf", "#4000bf", "#bf0080", "#7f4040", "#7f8040", "#408040", "#005580", "#2b0080", "#800055", "#ff5500", "#aaff00", "#00ff55", "#40bfff", "#8040ff", "#ff40bf", "#bf4000", "#7fbf00", "#00bf40", "#308fbf", "#6030bf", "#bf308f", "#7f2b00", "#558000", "#00802b", "#206080", "#402080", "#802060", "#ff8040", "#bfff40", "#40ff80", "#80d5ff", "#aa80ff", "#ff80d5", "#bf6030", "#8fbf30", "#30bf60", "#609fbf", "#8060bf", "#bf609f", "#7f4020", "#608020", "#208040", "#406a80", "#554080", "#80406a", "#ffaa80", "#d4ff80", "#7fffaa", "#0055ff", "#aa00ff", "#ff0055", "#bf8060", "#9fbf60", "#60bf80", "#0040bf", "#8000bf", "#bf0040", "#7f5540", "#6a8040", "#408055", "#002a80", "#550080", "#7f002b", "#ffaa00", "#55ff00", "#00ffaa", "#4080ff", "#bf40ff", "#ff4080", "#bf8000", "#40bf00", "#00bf80", "#3060bf", "#8f30bf", "#bf3060", "#7f5500", "#2a8000", "#008055", "#204080", "#602080", "#7f2040", "#ffbf40", "#7fff40", "#40ffbf", "#80aaff", "#d580ff", "#ff80aa", "#bf8f30", "#60bf30", "#30bf8f", "#6080bf", "#9f60bf", "#bf6080", "#7f6020", "#408020", "#208060", "#405580", "#6a4080", "#804055", "#ffd580", "#aaff80", "#80ffd4", "#0000ff", "#ff00ff", "#bf9f60", "#80bf60", "#60bf9f", "#0000bf", "#bf00bf", "#7f6a40", "#558040", "#40806a", "#000080", "#800080" };
        Color[] teklaColors = { new Color(1, 0, 0), new Color(1, 1, 0), new Color(0, 1, 0), new Color(0, 1, 1), new Color(0.25, 0.25, 1), new Color(1, 0.25, 1), new Color(0.75, 0, 0), new Color(0.75, 0.75, 0), new Color(0, 0.75, 0), new Color(0, 0.75, 0.75), new Color(0.188, 0.188, 0.75), new Color(0.75, 0.188, 0.75), new Color(0.496, 0, 0), new Color(0.496, 0.5, 0), new Color(0, 0.5, 0), new Color(0, 0.5, 0.5), new Color(0.125, 0.125, 0.5), new Color(0.5, 0.125, 0.5), new Color(1, 0.25, 0.25), new Color(1, 1, 0.25), new Color(0.25, 1, 0.25), new Color(0.5, 1, 1), new Color(0.5, 0.5, 1), new Color(1, 0.5, 1), new Color(0.75, 0.188, 0.188), new Color(0.75, 0.75, 0.188), new Color(0.188, 0.75, 0.188), new Color(0.375, 0.75, 0.75), new Color(0.375, 0.375, 0.75), new Color(0.75, 0.375, 0.75), new Color(0.496, 0.125, 0.125), new Color(0.496, 0.5, 0.125), new Color(0.125, 0.5, 0.125), new Color(0.25, 0.5, 0.5), new Color(0.25, 0.25, 0.5), new Color(0.5, 0.25, 0.5), new Color(1, 0.5, 0.5), new Color(1, 1, 0.5), new Color(0.496, 1, 0.5), new Color(0, 0.664, 1), new Color(0.332, 0, 1), new Color(1, 0, 0.664), new Color(0.75, 0.375, 0.375), new Color(0.75, 0.75, 0.375), new Color(0.375, 0.75, 0.375), new Color(0, 0.5, 0.75), new Color(0.25, 0, 0.75), new Color(0.75, 0, 0.5), new Color(0.496, 0.25, 0.25), new Color(0.496, 0.5, 0.25), new Color(0.25, 0.5, 0.25), new Color(0, 0.332, 0.5), new Color(0.168, 0, 0.5), new Color(0.5, 0, 0.332), new Color(1, 0.332, 0), new Color(0.664, 1, 0), new Color(0, 1, 0.332), new Color(0.25, 0.75, 1), new Color(0.5, 0.25, 1), new Color(1, 0.25, 0.75), new Color(0.75, 0.25, 0), new Color(0.496, 0.75, 0), new Color(0, 0.75, 0.25), new Color(0.188, 0.556, 0.75), new Color(0.375, 0.188, 0.75), new Color(0.75, 0.188, 0.556), new Color(0.496, 0.168, 0), new Color(0.332, 0.5, 0), new Color(0, 0.5, 0.168), new Color(0.125, 0.375, 0.5), new Color(0.25, 0.125, 0.5), new Color(0.5, 0.125, 0.375), new Color(1, 0.5, 0.25), new Color(0.75, 1, 0.25), new Color(0.25, 1, 0.5) };

        public PhaseManager()
        {
            myModel = new Model();
            updatePhaseViewList();
            RegisterEventHandler();
        }

        private static List<Reinforcement> GetReinforcementList(Model model)
        {
            ObjectFilterExpressions.Type objectType = new ObjectFilterExpressions.Type();
            NumericConstantFilterExpression type = new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.REBAR);
            BinaryFilterExpression expression = new BinaryFilterExpression(objectType, NumericOperatorType.IS_EQUAL, type);
            ModelObjectEnumerator.AutoFetch = true;
            ModelObjectEnumerator enumerator = model.GetModelObjectSelector().GetObjectsByFilter(expression);
            var list = new List<Reinforcement>();
            while (enumerator.MoveNext())
            {
                list.Add((Reinforcement)enumerator.Current);
            }
            return list;
        }

        private void updatePhaseViewList()
        {
            List<Reinforcement> ReinforcementList = GetReinforcementList(myModel);

            var queryReinforcemenByPhase =
                (from rf in ReinforcementList
                 let phase = rf.WhatIsMyPhase()
                 group rf by new { number = phase.PhaseNumber, name = phase.PhaseName, comment = phase.PhaseComment, current = phase.IsCurrentPhase }
                ).ToList();

            phaseViewList = new List<PhaseView>();

            foreach (var phaseGroup in queryReinforcemenByPhase)
            {
                List<Reinforcement> reinforcementList = new List<Reinforcement>();
                foreach (Reinforcement rf in phaseGroup)
                    reinforcementList.Add(rf);
                int index = queryReinforcemenByPhase.IndexOf(phaseGroup);
                phaseViewList.Add(new PhaseView(true, hexColors[index], teklaColors[index], phaseGroup.Key.number, phaseGroup.Key.name, phaseGroup.Key.comment, phaseGroup.Key.current, reinforcementList));
            }

            phaseViewList.OrderBy(PhaseView => PhaseView.Number);
        } 

        public void SetTemporaryColor()
        {
            ModelObjectVisualization.ClearAllTemporaryStates();

            ModelObjectVisualization.SetTemporaryStateForAll(new Color(0.5, 0.5, 0.5));

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
            }
        }


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
                
            }
        }

        void Events_ModelObjectChangedEvent(List<ChangeData> changes)
        {
            /* Make sure that the inner code block is running synchronously */
            lock (_changedObjectHandlerLock)
            {
                updatePhaseViewList();
                OnPropertyChanged("PhaseViewList");
                SetTemporaryColor();
            }
        }

    }
}
