using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;


namespace RebarPhasesManager
{
    public static class ModelExtensionMethods
    {
        public static Phase WhatIsMyPhase(this ModelObject mO)
        {
            Phase phase;
            if (mO.GetPhase(out phase))
                return phase;
            else return null;
        }

        public static string WhatIsMyUserComment(this Phase phase, Dictionary<string, string> phaseCodes)
        {
            string comment;
            string number = phase.PhaseNumber.ToString();
            string last2Char = number.Substring(Math.Max(0, number.Length - 2));
            try
            {
                comment = phaseCodes[last2Char];
            }
            catch (KeyNotFoundException)
            {
                return "***";
            }

            if (phase.GetUserProperty("PHASE_COM", ref comment))
                return comment;
            else
                return "***";

        }
    }

}
