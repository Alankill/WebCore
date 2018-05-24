using Application.DTO.Tasks;
using Application.IAppService;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.EntityFramework;
using Web.EntityFramework.Uow;

namespace Application.AppService
{
    public class TaskAppService:ApplicationService,ITaskAppService
    {
        private readonly IRepository<Core.Domain.Tasks.Task> _taskRepository;
        public TaskAppService(IUnitOfWork uow,IRepository<Core.Domain.Tasks.Task> taskRepositoy):base(uow)
        {
            _taskRepository = taskRepositoy;
        }

        public async Task<List<TaskListOutput>> GetAll(TaskListInput input)
        {
            var tasks = await _taskRepository.GetAll().WhereIf(input!=null,m=>m.State==input.State).ToListAsync();
            return Mapper.Map<List<TaskListOutput>>(tasks);
        }
    }
}
