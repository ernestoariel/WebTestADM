using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Food.Common;

namespace Food.Services.DTOs
{
    public class MeasureDTOValidator : AbstractValidator<MeasureDTO>
    {
        public MeasureDTOValidator()
        {
            RuleFor(dto => dto.Description).NotEmpty().WithErrorCode(ErrorCode.FIELD_IS_EMPTY);
            RuleFor(dto => dto.Calories).NotEmpty().WithErrorCode(ErrorCode.FIELD_IS_EMPTY);
            RuleFor(dto => dto.TotalFat).NotEmpty().WithErrorCode(ErrorCode.FIELD_IS_EMPTY);
        }
    }
}
