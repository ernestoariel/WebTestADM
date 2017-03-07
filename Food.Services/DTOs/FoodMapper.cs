using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Services.DTOs
{
    public static class FoodMapper
    {
        public static Core.Food ToEntity(this FoodDTO foodDto)
        {
            if (foodDto == null) return null;
            var measureList = foodDto.MeasureDtos?.Select(dto => dto.ToEntity()).ToList();
            return new Core.Food
            {
                FoodId = foodDto.FoodId,
                Description = foodDto.Description,
                Measures = measureList
            };
        }

        public static FoodDTO ToDto(this Core.Food food)
        {
            if (food == null) return null;
            var measureDtoList = food.Measures?.Select(entity => entity.ToDto()).ToList();
            return new FoodDTO
            {
                Description = food.Description,
                FoodId = food.FoodId,
                MeasureDtos = measureDtoList
            };
        }
    }
}
