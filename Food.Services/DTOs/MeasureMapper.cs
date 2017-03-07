using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Core;

namespace Food.Services.DTOs
{
    public static class MeasureMapper
    {
        public static Core.Measure ToEntity(this MeasureDTO measureDto)
        {
            if (measureDto == null) return null;
            return new Measure
            {
                Calories = measureDto.Calories,
                Description = measureDto.Description,
                MeasureId = measureDto.MeasureId,
                TotalFat = measureDto.TotalFat
            };
        }

        public static MeasureDTO ToDto(this Measure measure)
        {
            if (measure == null) return null;
            return new MeasureDTO
            {
                Calories = measure.Calories,
                Description = measure.Description,
                MeasureId = measure.MeasureId,
                TotalFat = measure.TotalFat
            };
        }
    }
}
