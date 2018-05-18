using Application.AutoMapper;
using AutoMapper;
using Core.Logger;
using log4net;


namespace Application
{
    public class ApplicationService
    {
        public IMapper Mapper { get; private set; }
        public ILog Logger { get { return logger; }}
        private ILog logger;
        public ApplicationService()
        {
            Mapper = AutoMapperManager.Mapper;
            logger = LoggerManager.GetLogger(this.GetType());
        }
    }
}
