using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;


namespace RebarPhaseManager
{
    public static class TeklaExtensionMethods
    {
        public static Phase WhatIsMyPhase(this ModelObject mO)
        {
            Phase phase;
            if (mO.GetPhase(out phase))
                return phase;
            else return null;
        }

        public static string WhatIsMyComment(this Phase phase)
        {
            string comment = "";

            if (phase.GetUserProperty("PHASE_COM", ref comment))
                return comment;
            else
            {
                comment = phase.PhaseComment;
                return comment;
            }
                

        }
    }

}
