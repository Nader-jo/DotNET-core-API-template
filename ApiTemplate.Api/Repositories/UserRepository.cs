using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using ApiTemplateDbContext = ApiTemplate.Api.Database.ApiTemplateDbContext;

namespace ApiTemplate.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiTemplateDbContext _context;

        public UserRepository(ApiTemplateDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(Guid userId) => await _context.Users.FirstOrDefaultAsync(m => m.Id == userId);

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async void Update() => await _context.SaveChangesAsync();

        public async Task<User> GetByEmail(string email) =>
            await _context.Users.FirstOrDefaultAsync(m => m.Email == email);
    }
}