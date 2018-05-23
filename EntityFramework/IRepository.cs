using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web.EntityFramework
{
    //接口应在core中定义 Application应只引用core中接口而无需应用EF
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get;}

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<(List<TEntity> List, int TotalCount, int TotalPage)> Page<T>(int pagenum, int pagesize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> order, bool isDesc);

        Task<TEntity> Find(object id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);
        void AddRange(params TEntity[] entities);

        TEntity Update(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        void Delete<T>(object Id);
        void Delete(TEntity entity);
    }
}
