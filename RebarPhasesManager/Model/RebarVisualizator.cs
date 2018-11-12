using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhasesManager.Model
{
    public static class RebarVisualizator
    {
        public static void SetTempColor(List<Reinforcement> rebarList, Color color)
        {
            List<ModelObject> modelObjects = new List<ModelObject>();
            foreach (Reinforcement rebar in rebarList)
            {
                modelObjects.Add(rebar);
            }
            ModelObjectVisualization.SetTemporaryState(modelObjects, color);
        }

        public static void SetTempColor(Reinforcement rebar, Color color)
        {
            List<ModelObject> modelObjects = new List<ModelObject>();
            modelObjects.Add(rebar);
            ModelObjectVisualization.SetTemporaryState(modelObjects, color);
        }
    }
}
