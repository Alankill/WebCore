using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;
using Core.IRepository;
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
    public class EfRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        protected DbContext Context;
        protected DbSet<TEntity> Table;
        public EfRepository(DbContext _context)
        {
            Context = _context;
            Table = _context.Set<TEntity>();
        }


        #region
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
        public async Task<List<TEntity>> ToListAsync(IQueryable<TEntity> list)
        {
            return await list.ToListAsync<TEntity>();
        }


        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public  async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public List<TEntity> Page<T>(int pagenum, int pagesize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> order, bool isDesc, out int totalcount, out int totalpagecount)
        {
            totalcount = Table.Count(where);
            totalpagecount = totalcount / pagesize + (totalcount % pagesize == 0 ? 0 : 1);
            if (isDesc)
            {
                return Table.Where(where).OrderByDescending(order).Skip((pagenum - 1) * pagesize).Take(pagesize).ToList();

            }
            else
            {
                return Table.Where(where).OrderBy(order).Skip((pagenum - 1) * pagesize).Take(pagesize).ToList();
            }

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
            EntityEntry ee=AttachIfNot(entity);
            ee.State = EntityState.Modified;
            return entity;
        }

        public void Update(TEntity entity, string properties)
        {
            EntityEntry ee = AttachIfNot(entity);
            string[] arr = properties.Split(",");
            foreach (string p in arr)
            {
                ee.Property(p).IsModified = true;
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
                Table.Attach(entity).Property("IsDelete").IsModified = true;
            }
            else
            {
                AttachIfNot(entity);
                Table.Remove(entity);
            }
        }

        public  async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }
        public  async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }
        public  async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }
        public  async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }


        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        protected virtual EntityEntry AttachIfNot(TEntity entity)
        {
            EntityEntry entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return entry;
            }
            return Table.Attach(entity);
        }
        #endregion
    }
}
