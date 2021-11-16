using System.Threading.Tasks;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using ApiTemplate.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApiTemplateDbContext _context;

        public UserRepository(ApiTemplateDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email) =>
            await _context.Set<User>().FirstOrDefaultAsync(m => m.Email == email);

    }
}