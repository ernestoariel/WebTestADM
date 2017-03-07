using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Food.Services.DTOs
{
    public interface IFoodDTOValidatorFactory
    {
        IValidator<FoodDTO> Create(bool isCreation);
        void Release(IValidator<FoodDTO> foodDtoValidator);
    }
}
