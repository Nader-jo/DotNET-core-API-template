using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using FluentValidation;
using MediatR;
using static ApiTemplate.Contract.Errors;

namespace ApiTemplate.Application.Commands
{
    public record AddUserCommand(string Name, string Email, string Role) : IRequest<Guid>;

    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is empty");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is empty");
            RuleFor(r => r.Role).NotEmpty().WithMessage("Role is empty");
        }

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
        {
            private readonly IUserRepository _userRepository;

            public AddUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

            public async Task<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
            {
                var (name, email, role) = command;
                var userDb = await _userRepository.GetByEmail(email);
                if (userDb != null) return await ThrowError("Email address already used with another user.");
                var user = new User(name, email, role);
                await _userRepository.Add(user);
                return await Task.FromResult(user.Id);

            }
        }
    }
}