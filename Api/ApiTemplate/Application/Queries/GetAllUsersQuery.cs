using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Contract;
using ApiTemplate.Domain.Repository;
using AutoMapper;
using MediatR;

namespace ApiTemplate.Application.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery command, CancellationToken cancellationToken)
        {
            var users = await _userRepository.List();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return await Task.FromResult(usersDto);
        }
    }
}