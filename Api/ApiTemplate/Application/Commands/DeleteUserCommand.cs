using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Domain.Repository;
using FluentValidation;
using MediatR;
using static ApiTemplate.Contract.Errors;

namespace ApiTemplate.Application.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest<Guid>;

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Id is empty");
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Guid> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult.ToString());

            var user = await _userRepository.Get(command.Id);
            if (user == null) return await ThrowError("No user with the provided user Id is found.");
            _userRepository.Delete(user);
            return command.Id;
        }
    }
}