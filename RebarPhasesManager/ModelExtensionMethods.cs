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
        public static Phase WhatIsMyPhase (this Reinforcement rf)
        {
            Phase phase;
            if (rf.GetPhase(out phase))
                return phase;
            else return null;
        }
    }
}
