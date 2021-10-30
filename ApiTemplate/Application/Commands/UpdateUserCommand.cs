using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Application.Commands
{
    public record UpdateUserCommand(Guid Id, string Name, string Email, string Role) : IRequest<Guid>;

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Id is empty");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is empty");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is empty");
            RuleFor(r => r.Role).NotEmpty().WithMessage("Role is empty");
        }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
        {
            private readonly IUserRepository _userRepository;

            public UpdateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Guid> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                var (id, name, email, role) = command;
                var user = await _userRepository.Get(id);
                user.Update(name, email, role);
                _userRepository.Update();
                return await Task.FromResult(user.Id);
            }
        }
    }
}