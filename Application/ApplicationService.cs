using Application.AutoMapper;
using AutoMapper;
using Core.Logger;
using log4net;
using System;
using System.Threading.Tasks;
using Web.EntityFramework.Uow;

namespace Application
{
    public class ApplicationService:IApplicationService
    {
        public IMapper Mapper { get; private set; }
        public ILog Logger { get; private set; }

        public IUnitOfWork Uow { get; private set; }
        public Exception ErrorInfo { get { return Uow.ErrorInfo; } }
        public bool SaveAllChange()
        {
            Uow.SaveChanges();
            return Uow.IsSuccessed;
        }
        public async Task<bool> SaveChangesAsync()
        {
           await Uow.SaveChangesAsync();
           return Uow.IsSuccessed;
        }

        public ApplicationService(IUnitOfWork uow)
        {
            Mapper = AutoMapperManager.Mapper;
            Logger = LoggerManager.GetLogger(this.GetType());
            Uow = uow;
        }
    }
}
