using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Web.EntityFramework.Uow
{
    public class UnitOfWork:IUnitOfWork
    {
        private DbContext context;
        public UnitOfWork(DbContext dbContext)
        {
            context = dbContext;
        }

        public DbContext GetContext()
        {
            return context;
        }

        public bool IsSuccessed { get; private set; }
        public Exception ErrorInfo { get; private set; }

        public int SaveChanges()
        {
            try
            {
                int num = context.SaveChanges();
                IsSuccessed = true;
                return num;

            }
            catch (Exception ex)
            {
                this.ErrorInfo = ex;
                IsSuccessed = false;
                return -1;
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var task = await context.SaveChangesAsync();
                IsSuccessed = true;
                return task;
            }
            catch (Exception ex)
            {
                this.ErrorInfo = ex;
                IsSuccessed = false;
                return -1;
            }
        }
    }
}
