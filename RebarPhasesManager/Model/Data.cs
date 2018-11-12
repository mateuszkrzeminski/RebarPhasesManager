﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhasesManager.Model
{
    public static class Data
    {
        public static Color[] TeklaColors = { new Color(1,0,0), new Color(1,1,0), new Color(0.33,1,0),  new Color(0.17,0,1), new Color(0,1,0.5), new Color(0,0.66,1), new Color(1,0,1),
                                new Color(1,0.5,0.5), new Color(1,0.91,0.5), new Color(0.66,1,0.5), new Color(0.58,0.5,1), new Color(0.5,1,0.75), new Color(0.5,0.83,1), new Color(1,0.5,1),
                                new Color(1,0.25,0.25), new Color(1,0.87,0.25), new Color(0.5,1,0.25), new Color(0.38,0.25,1), new Color(0.25,1,0.62), new Color(0.25,0.75,1), new Color(1,0.25,1) };

        public static Color InvisibleColor = new Color(0.5, 0.5, 0.5);
        public static Color NotAnalyzedColor = new Color(0.3, 0.3, 0.3, 0.5);

        public static Dictionary<string, string> PhaseCodes = new Dictionary<string, string>()
        {
            { "00", "CL" },
            { "01", "F" },
            { "02", "APTh" },
            { "03", "APDe" },

            { "04", "AD" },
            { "05", "OLINK" },
            { "06", "CLINK" },
            { "07", "CO" },
            { "08", "TB" },
            { "09", "ST" },
            { "10", "MESH" },

            { "11", "NF0" },
            { "12", "FF0" },
            { "13", "TO0" },
            { "14", "BO0" },
            { "15", "S0" },

            { "17", "POB" },
            { "18", "RE" },

            { "21", "NF1" },
            { "22", "FF1" },
            { "23", "TO1" },
            { "24", "BO1" },
            { "25", "S1" },

            { "31", "NF2" },
            { "32", "FF2" },
            { "33", "TO2" },
            { "34", "BO2" },
            { "35", "S2" },

            { "41", "NF3" },
            { "42", "FF3" },
            { "43", "TO3" },
            { "44", "BO3" },
            { "45", "S3" },

            { "51", "NF4" },
            { "52", "FF4" },
            { "53", "TO4" },
            { "54", "BO4" },
            { "55", "S4" },

            { "96", "Misc1" },
            { "97", "Misc2" },
            { "98", "Misc3" }
        };
    }
}