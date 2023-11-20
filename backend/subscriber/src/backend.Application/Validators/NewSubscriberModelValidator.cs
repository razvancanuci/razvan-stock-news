using backend.Application.Models;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace backend.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class NewSubscriberModelValidator : AbstractValidator<NewSubscriberModel>
    {
        public NewSubscriberModelValidator()
        {
            RulesForEmail();
            RulesForName();
            RulesForPhoneNumber();
        }

        private void RulesForEmail()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email must be not null");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be not empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must have the email address format");
        }

        private void RulesForName()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name must be not null");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be not empty");
            RuleFor(x => x.Name).Must(ValidateName).WithMessage("Name must not have symbols or numbers");
        }
        
        private bool ValidateName(string? name)
        {
            return !(name?.Any(char.IsDigit) == true ||
                name?.Any(char.IsSymbol) == true);
        }

        private void RulesForPhoneNumber()
        {
            RuleFor(x => x.PhoneNumber).Must(ValidPhoneNumber).WithMessage("There must be a valid romanian phone number");
        }

        private bool ValidPhoneNumber(string? phoneNumber)
        {
            Regex rx = new(
           @"^07[0-8]{1}[0-9]{7}$");
            if (phoneNumber == null)
            {
                return true;
            }
            return rx.Match(phoneNumber).Success || phoneNumber.Equals("");
        }
    }
}
