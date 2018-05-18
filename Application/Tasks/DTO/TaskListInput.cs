using Core.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Tasks.DTO
{
    public class TaskListInput
    {
        public TaskState? State { get; set; }
    }
}
