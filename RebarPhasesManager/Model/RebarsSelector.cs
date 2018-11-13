using System;
using System.Collections;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhaseManager.Model
{
    class RebarsSelector : IEnumerable
    {
        #region Members
        private ModelObjectEnumerator modelObjectEnumerator;
        private Tekla.Structures.Model.UI.ModelObjectSelector selector;
        private Picker picker;
        #endregion

        #region Construction
        public RebarsSelector()
        {
            ModelObjectEnumerator.AutoFetch = true;
            selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            picker = new Picker();
        }
        #endregion


        #region Methods
        private void getEnumerator()
        {
            modelObjectEnumerator = selector.GetSelectedObjects();
            if (modelObjectEnumerator.GetSize() > 0)
                return;
            else
            {
                try
                {
                    modelObjectEnumerator = picker.PickObjects(Picker.PickObjectsEnum.PICK_N_REINFORCEMENTS);
                }
                catch (Exception)
                {
                    MessageBox.Show("No rebars selected");
                    return;
                }
            }
        }

        public void SelectRebars(ArrayList rebarsToSelect)
        {
            selector.Select(rebarsToSelect);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            getEnumerator();
            foreach (ModelObject modelObject in modelObjectEnumerator)
            {
                if (modelObject is Reinforcement)
                    yield return modelObject;
            }
        }

        #endregion


    }
}
