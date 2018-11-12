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
            //RegisterEventHandler();
        }
        #endregion

        #region Methods

        #endregion











        //// Tekla Events handling

        //Events _events = new Events();
        //private object _selectionEventHandlerLock = new object();
        //private object _changedObjectHandlerLock = new object();

        //public void RegisterEventHandler()
        //{
        //    _events.SelectionChange += Events_SelectionChangeEvent;
        //    _events.ModelObjectChanged += Events_ModelObjectChangedEvent;
        //    _events.Register();
        //}

        //public void UnRegisterEventHandler()
        //{
        //    _events.UnRegister();
        //}

        //void Events_SelectionChangeEvent()
        //{
        //    /* Make sure that the inner code block is running synchronously */
        //    lock (_selectionEventHandlerLock)
        //    {
        //        //getSelectedModelObjectsEnumerator();
        //    }
        //}

        //void Events_ModelObjectChangedEvent(List<ChangeData> changes)
        //{
        //    /* Make sure that the inner code block is running synchronously */
        //    lock (_changedObjectHandlerLock)
        //    {
        //        ModelObjectVisualization.ClearAllTemporaryStates();
        //        phaseViewList = null;
        //        OnPropertyChanged("PhaseViewList");
        //        createReinforcementList();
        //        createPhaseViewList(reinforcements);
        //        OnPropertyChanged("PhaseViewList");
        //        SetTemporaryColor();
        //    }
        //}

    }
}
