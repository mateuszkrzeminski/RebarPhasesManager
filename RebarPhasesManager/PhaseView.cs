using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhasesManager
{
    class PhaseView
    {
        public bool Visible { get; set; }
        public string HexColor { get; private set; }
        public Color TeklaColor { get; private set; }
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public int Current { get; private set; }

        public List<Reinforcement> ReinforcementObjectsList { get; private set; }

        public PhaseView (bool visible, string hexColor, Color teklaColor, int number, string name, string comment, int current, List<Reinforcement> reinforcementObjectsList)
        {
            Visible = visible;
            HexColor = hexColor;
            TeklaColor = teklaColor;
            Number = number;
            Name = name;
            Comment = comment;
            Current = current;
            ReinforcementObjectsList = reinforcementObjectsList;
        }
    }
}
