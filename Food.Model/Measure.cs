﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Model
{
    /// <summary>
    /// Food measure, a piece, 1 pound, 1 Kilo                
    /// </summary>
    public class Measure
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int TotalFat { get; set; }
        public int SaturatedFat { get; set; }
        public int Protein { get; set; }
        public int Sugar { get; set; }
        public int Sodium { get; set; }

        public Food Food { get; set; }
    }
}
