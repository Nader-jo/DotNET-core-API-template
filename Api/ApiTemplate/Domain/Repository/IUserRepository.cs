using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;

namespace ApiTemplate.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> Get(Guid userId);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAll();
        Task Add(User user);
        Task Delete(User user);
        void Update();
    }
}