namespace CarAuction.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static System.Globalization.DateTimeStyles;

    public class ValidDateAttribute : ValidationAttribute
    {
        public string ErrorMessage { get; set; }
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var isValidDate = DateTime.TryParseExact(value.ToString(), "yyyy", CultureInfo.InvariantCulture,
                None, out DateTime date);

            if (date.Year <= DateTime.UtcNow.Year)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Year is after {DateTime.UtcNow.Year}");
        }
    }
}
