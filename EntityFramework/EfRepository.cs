using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Web.EntityFramework
{
    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class//,IEntity<>
    {
        private DbContext Context;
        public DbSet<TEntity> Table { get;private set; }
        public EfRepository(DbContext _context)
        {
            Context = _context;
            Table = _context.Set<TEntity>();
        }
    
        
        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }
        public  IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors == null || propertySelectors.Count() <= 0)
            {
                return GetAll();
            }

            var query = GetAll();

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync().ConfigureAwait(false);
        }
        public  async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync().ConfigureAwait(false);
        }
        public async Task<(List<TEntity> List,int TotalCount,int TotalPage)> Page<T>(int pagenum, int pagesize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> order, bool isDesc)
        {
            List<TEntity> list;
            var totalcount = await Table.CountAsync(where);
            var totalpagecount = totalcount / pagesize + (totalcount % pagesize == 0 ? 0 : 1);
            if (isDesc)
            {
                list=await Table.Where(where).OrderByDescending(order).Skip((pagenum - 1) * pagesize).Take(pagesize).ToListAsync();

            }
            else
            {
                list = await Table.Where(where).OrderBy(order).Skip((pagenum - 1) * pagesize).Take(pagesize).ToListAsync();
            }
            return (List:list,TotalCount:totalcount,TotalPage:totalpagecount);
        }

        public async Task<TEntity> Find(object id)
        {
            return await Table.FindAsync(id);
        }
        public  async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }
        public  async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            return (Table.Add(entity)).Entity;
        }
        public void  AddRange(params TEntity[] entities)
        {
              Table.AddRange(entities);
        }

        public  TEntity Update(TEntity entity)
        {
            EntityEntry ee = Context.Entry<TEntity>(entity);
            ee.State = EntityState.Modified;
            return entity;
        }
        public void Update(TEntity entity,params Expression<Func<TEntity,object>>[] properties)
        {
            EntityEntry ee = Context.Entry<TEntity>(entity);
            foreach (var property in properties)
            {
                var propertyName = property.ReturnType.Name;
                ee.Property(propertyName).IsModified = true;
            }
        }

        public void Delete<T>(object Id)
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            ((IEntity<T>)entity).ID = (T)Id;
            if (entity != null)
            {
                if (entity is IIsDelete)
                {
                    ((IIsDelete)entity).IsDelete = true;
                    Table.Attach(entity).Property("IsDelete").IsModified = true;
                }
                else
                {
                    Table.Remove(entity);
                }
            }
        }
        public void Delete(TEntity entity)
        {
            if (entity is IIsDelete)
            {
                ((IIsDelete)entity).IsDelete = true;
                Context.Entry<TEntity>(entity).Property("IsDelete").IsModified = true;
            }
            else
            {
                Context.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
        }
    }
}
