using ServiceContract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Validates the provided model object using data annotations.
        /// </summary>
        /// <param name="model">The object to be validated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the validation of the model fails.
        /// The exception message contains the error message from the first validation error encountered.
        /// </exception>
        public static void validationModel(object model)
        {
            ValidationContext personValidationContext = new ValidationContext(model);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, personValidationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
