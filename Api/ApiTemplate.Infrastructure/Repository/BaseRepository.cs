using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApiTemplate.Common.Interfaces;
using ApiTemplate.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApiTemplateDbContext _context;

        protected BaseRepository(ApiTemplateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> List() => await _context.Set<T>().ToListAsync();

        public async Task<T> Get(Guid id) => await _context.Set<T>().FindAsync(id);

        public async Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null) query = includes.Aggregate(query, (current, incluse) => current.Include(incluse));

            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int take, int skip) =>
            await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            var query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                query = orderByDirection == "ASC" ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.SaveChanges();
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> Count() => await _context.Set<T>().CountAsync();

        public async Task<int> Count(Expression<Func<T, bool>> criteria) =>
            await _context.Set<T>().CountAsync(criteria);
    }
}