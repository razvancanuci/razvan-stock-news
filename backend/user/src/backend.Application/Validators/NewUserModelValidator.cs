using backend.Application.Models;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class NewUserModelValidator : AbstractValidator<NewUserModel>
    {
        public NewUserModelValidator()
        {
            RulesForUsername();
            RulesForPassword();
        }
        private void RulesForUsername()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Username can't be null");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can't be empty");
            RuleFor(x => x.Username).Length(5, 20).WithMessage("Username should have length between 5 and 20");
        }
        private void RulesForPassword()
        {
            RuleFor(x => x.Password).NotNull().WithMessage("Password can't be null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty");
            RuleFor(x => x.Password).Length(5, 20).WithMessage("Password should have length between 5 and 20");
            RuleFor(x => x.Password).Must(ValidatePassword).WithMessage("Password must contain a number, an uppercase letter and a symbol");

        }
        private bool ValidatePassword(string password)
        {
            return !(password?.Any(char.IsDigit) == false ||
                password?.Any(char.IsUpper) == false ||
                password?.Any(char.IsSymbol) == false ||
                password?.Any(char.IsLower) == false);
        }
    }
}
