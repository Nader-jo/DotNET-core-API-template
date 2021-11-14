using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;

namespace ApiTemplate.Services
{
    public interface IUserService
    {
        Task<User> Get(Guid userId);
        Task<List<User>> GetAll();
    }
}