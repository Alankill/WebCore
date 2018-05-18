using Application.Tasks.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks
{
    public interface ITaskAppService:IApplicationService
    {
        Task<List<TaskListOutput>> GetAll(TaskListInput input);
    }
}
