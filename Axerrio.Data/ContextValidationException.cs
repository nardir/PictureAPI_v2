using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public class ContextValidationException: Exception
    {
        public readonly IEnumerable<ValidationResult> ValidationResults;

        public ContextValidationException(IEnumerable<ValidationResult> validationResults): base("Context Validation Exception")
        {
            ValidationResults = validationResults;
        }
    }
}
