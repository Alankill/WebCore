using Application.Tasks.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Repository;
using Core.Domain.Tasks;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public class TaskAppService:ApplicationService,ITaskAppService
    {
        private readonly IRepository<Core.Domain.Tasks.Task, int> _taskRepository;
        public TaskAppService(IRepository<Core.Domain.Tasks.Task, int> taskRepositoy)
        {
            _taskRepository = taskRepositoy;
        }

        public async Task<List<TaskListOutput>> GetAll(TaskListInput input)
        {
            var tasks = await _taskRepository.GetAllListAsync();
            Logger.Warn("avbccc");
            return new List<TaskListOutput>(Mapper.Map<List<TaskListOutput>>(tasks));
        }
    }
}
