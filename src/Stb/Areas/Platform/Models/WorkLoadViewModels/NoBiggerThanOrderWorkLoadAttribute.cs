using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.WorkLoadViewModels
{
    public class NoBiggerThanOrderWorkLoadAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            WorkLoadViewModel workLoad = (WorkLoadViewModel)validationContext.ObjectInstance;

            if (workLoad.Amount != null && workLoad.OrderAmount != 0 && workLoad.Amount.Value > workLoad.OrderAmount)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;

        }
    }
}
