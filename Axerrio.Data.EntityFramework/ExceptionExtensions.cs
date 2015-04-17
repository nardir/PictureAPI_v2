using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    internal static class ExceptionExtensions
    {
        public static ContextValidationException ConvertToContextValidationException(this DbEntityValidationException exception)
        {
            var entityValidationResults = exception.EntityValidationErrors;
            IEnumerable<ValidationResult> validationResults =
                entityValidationResults.SelectMany(
                    evr =>
                        evr.ValidationErrors.Select(
                            ve => new ValidationResult(ve.ErrorMessage, new[] { ve.PropertyName }))).ToList();

            return new ContextValidationException(validationResults);
        }
    }
}
