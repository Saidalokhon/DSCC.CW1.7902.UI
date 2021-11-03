using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DSCC.CW1._7902.UI.Models
{
    // Create model for Employee, that implements IModel.
    public class Employee : IModel, IValidatableObject
    {
        #region Variables
        // Implement the interface member 'Id'.
        public int Id { get; set; }
        // Create the rest of the required variables.
        [Required]
        [Display(Name = "Firstname"), MinLength(3), MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname"), MinLength(3), MaxLength(50)]
        public string LastName { get; set; }

        [Required, Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        public Position Position { get; set; }
        #endregion
        #region CustomValidation
        // Implementing custom validation.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validating Date of Birth of the employee.
            if (DateOfBirth < DateTime.Now.AddYears(-60))
                yield return new ValidationResult("Employee can't be older than 60 years old");
            if (DateOfBirth > DateTime.Now)
                yield return new ValidationResult("Date of birth can't be more than today's date");
            yield return ValidationResult.Success;
        }
        #endregion
    }
}
