using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiTemplate.Common.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(Guid tId);
        Task Add(T element);
        void Delete(T element);
        void Update(T element);
        Task<IEnumerable<T>> List();
        Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int skip, int take);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");

        Task AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> criteria);
    }
}