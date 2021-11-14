using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Common.Interfaces;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using ApiTemplate.Services;

namespace ApiTemplate.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(Guid userId) => await _userRepository.Get(userId);

        public async Task<List<User>> GetAll() => (List<User>) await _userRepository.List();
    }
}