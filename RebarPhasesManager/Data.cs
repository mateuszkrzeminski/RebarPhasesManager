using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace RebarPhaseManager
{
    public static class Data
    {
        public static Color[] TeklaColors = 
        {
            new Color(1,0,0),           new Color(1,1,0),           new Color(0,1,0),           new Color(0.5,0,0.5),       new Color(0,0,1),           new Color(0.5,0.25,0),
            new Color(0.65,0.14,0.15),  new Color(1,0.55,0.1),      new Color(0.05,0.23,0.18),  new Color(0.56,0.15,0.25),  new Color(0.34,0.55,0.71),  new Color(0.49,0.36,0.22),
            new Color(0.88,0.37,0.12),  new Color(1,0.84,0.3),      new Color(0.75,0.89,0.73),  new Color(0.79,0.22,0.55),  new Color(0,0.06,0.46),     new Color(0.43,0.23,0.19),
            new Color(0.78,0.09,0.07),  new Color(0.98,0.87,0.67),  new Color(0.2,0.47,0.33),   new Color(0.91,0.61,0.71),  new Color(0.49,0.8,0.74),   new Color(0.61,0.27,0.16),
            new Color(0.8,0.51,0.45),   new Color(0.99,0.74,0.12),  new Color(0.06,0.44,0.2),   new Color(0.39,0.24,0.61),  new Color(0.16,0.45,0.72),  new Color(0.52,0.22,0.17),
            new Color(0.85,0.4,0.46),   new Color(1,0.67,0.35),     new Color(0.14,0.57,0.25),  new Color(0.57,0.06,0.4),   new Color(0,0.03,0.31),     new Color(0.37,0.2,0.12),
            new Color(0.85,0.35,0.31),  new Color(0.89,0.64,0.16),  new Color(0.52,0.65,0.48),  new Color(0.22,0.04,0.18),  new Color(0.09,0.38,0.67),  new Color(0.39,0.24,0.14),
            new Color(0.83,0.27,0.16),  new Color(1,0.46,0.13),     new Color(0.5,0.5,0),       new Color(0.62,0.45,0.58),  new Color(0,0.23,0.5),      new Color(0.28,0.15,0.11),
            new Color(0.95,0.23,0.11),  new Color(0.97,0.6,0.36),   new Color(0.29,0.43,0.2),   new Color(0.36,0.03,0.17),  new Color(0.18,0.32,0.56),  new Color(0.33,0.12,0.12),
            new Color(0.98,0.31,0.16),  new Color(0.89,0.72,0.22),  new Color(0.07,0.47,0.15),  new Color(0.81,0.16,0.26),  new Color(0.22,0.58,0.51),  new Color(0.24,0.12,0.11),
        };

        public static Color InvisibleColor = new Color(0.35, 0.35, 0.35);
        public static Color NotAnalyzedColor = new Color(0.25, 0.25, 0.25, 0.3);

        public static string NoColorsAvailable = "No colors available. Another phase can not be added.";

    }
}
