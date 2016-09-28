using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Services.DTOs
{
    public class MeasureDTO
    {
        public int MeasureId { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int? TotalFat { get; set; }

        public static MeasureDTO FromMeasure(Core.Measure measure)
        {
            if (measure == null) return null;
            return new MeasureDTO
            {
                Description = measure.Description,
                Calories = measure.Calories,
                MeasureId = measure.MeasureId,
                TotalFat = measure.TotalFat
            };
        }

    }
}
