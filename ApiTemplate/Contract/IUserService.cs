using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;

namespace ApiTemplate.Contract
{
    public interface IUserService
    {
        Task<User> Get(Guid userId);
        Task<List<User>> GetAll();
    }
}