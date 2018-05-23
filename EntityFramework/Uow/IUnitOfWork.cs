using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Web.EntityFramework.Uow
{
    public interface IUnitOfWork
    {
        DbContext GetContext();
        Exception ErrorInfo { get; }
        bool IsSuccessed { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
