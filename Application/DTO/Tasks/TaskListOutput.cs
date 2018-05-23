using Core.Domain.Tasks;
using System;

namespace Application.DTO.Tasks
{
    public class TaskListOutput
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public TaskState State { get; set; }
        public DateTime CreatDate { get; set; }
        public string AssignedPersonName { get; set; }
    }
}
