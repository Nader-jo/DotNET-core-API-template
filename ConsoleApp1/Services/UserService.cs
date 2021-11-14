using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Contract;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using ApiTemplate.Services;

namespace ApiTemplate.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(Guid userId) => await _userRepository.Get(userId);

        public async Task<List<User>> GetAll() => await _userRepository.GetAll();
    }
}