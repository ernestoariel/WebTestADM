using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Food.Common;

namespace Food.Services.DTOs
{
    public class FoodDTOValidator : AbstractValidator<FoodDTO>
    {
        public FoodDTOValidator(bool isCreation)
        {
            if (isCreation)
            {
                RuleFor(food => food.FoodId).Empty().WithErrorCode(ErrorCode.NEW_ENTITY_ID_WITH_VALUE);
            }
            else
            {
                RuleFor(food => food.FoodId).NotEmpty().WithErrorCode(ErrorCode.FIELD_IS_EMPTY);
            }
            RuleFor(food => food.Description).NotEmpty().WithErrorCode(ErrorCode.FIELD_IS_EMPTY);

            RuleFor(dto => dto.MeasureDtos)
                .Must(p => !p.AreDuplicates(q => q.Description))
                .WithErrorCode(ErrorCode.DUPLICATED_VALUES)
                .WithMessage(ErrorCode.DUPLICATED_VALUES);
            RuleFor(dto => dto.MeasureDtos)
                .SetCollectionValidator(new MeasureDTOValidator())
                .WithErrorCode("MeasureDTOValidator failed");
        }
    }
}
