using AutoMapper;
using log4net;
using System;
using System.Threading.Tasks;
using Web.EntityFramework.Uow;

namespace Application
{
    public interface IApplicationService
    {
        IMapper Mapper { get;}
        ILog Logger { get; }
        IUnitOfWork Uow { get; }
        Exception ErrorInfo { get; }

        bool SaveAllChange();
        Task<bool> SaveChangesAsync();
    }
}
