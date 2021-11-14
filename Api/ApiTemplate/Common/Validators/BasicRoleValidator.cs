using ApiTemplate.Domain.Models;
using FluentValidation;

namespace ApiTemplate.Common.Validators
{
    public class BasicRoleValidator : AbstractValidator<string>
    {
        public BasicRoleValidator()
        {
            RuleFor(role => role)
                .Must(role => role.Equals(UserRole.User) || role.Equals(UserRole.Owner)).WithMessage("Role is wrong");
        }
    }
}