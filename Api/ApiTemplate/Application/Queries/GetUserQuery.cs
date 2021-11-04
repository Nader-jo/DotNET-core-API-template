using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Contract;
using ApiTemplate.Domain.Repository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Application.Queries
{
    public record GetUserQuery(Guid UserId) : IRequest<UserDto>;

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId is empty");
        }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(command.UserId);
            var usersDto = _mapper.Map<UserDto>(user);
            return await Task.FromResult(usersDto);
        }
    }
}