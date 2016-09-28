using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Services.DTOs
{
    public class FoodDTO
    {
        public int FoodId { get; set; }
        public string Description { get; set; }
        public ICollection<MeasureDTO> MeasureDtos { get; set; }

        public static FoodDTO FromFood(Core.Food food)
        {
            if (food == null) return null;
            return new FoodDTO
            {
                Description = food.Description,
                FoodId = food.FoodId,
                MeasureDtos = new List<MeasureDTO>(food.Measures.Select(p=> MeasureDTO.FromMeasure(p)))
            };
        }
    }
}
