using Application.DTO.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IAppService
{
    public interface ITaskAppService:IApplicationService
    {
        Task<List<TaskListOutput>> GetAll(TaskListInput input);
    }
}
