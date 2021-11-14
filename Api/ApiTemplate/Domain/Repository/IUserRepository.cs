using System.Threading.Tasks;
using ApiTemplate.Common.Interfaces;
using ApiTemplate.Domain.Models;

namespace ApiTemplate.Domain.Repository
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}