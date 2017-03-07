using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Food.Common
{
    public static class Extensions 
    {
        public static FluentValidation.ValidationException CreateError(
            this FluentValidation.ValidationException exception, string propertyName, string message, string errorCode)
        {
            return new FluentValidation.ValidationException(errorCode, new List<ValidationFailure>()
            {
                new ValidationFailure(propertyName, message) {ErrorCode = errorCode}
            });
        }

        public static GeneralError ToGeneralError(this FluentValidation.ValidationException exception)
        {
            var generalError = new GeneralError();
            foreach (var failure in exception.Errors)
            {
                if (generalError.Errors == null)
                {
                    generalError.Errors = new List<Error>();
                }
                generalError.Errors.Add(new Error
                {
                    ErrorCode = failure.ErrorCode,
                    UserMessage = failure.ErrorMessage,
                    PropertyName = failure.PropertyName,
                    Type = exception.GetType().ToString(),
                    DeveloperMessage = Properties.Settings.Default.IncludeExceptionDetail ? exception.StackTrace : null
                });
            }
            return generalError;
        }

        public static bool AreDuplicates<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.GroupBy(selector).Any(p => p.Count() > 1);
        }

        public static IEnumerable<TSource> GetDuplicates<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.GroupBy(selector).Where(p => p.Count() > 1).SelectMany(q => q);
        }
    }
}
