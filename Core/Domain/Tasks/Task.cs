using Core.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Tasks
{
    [Table("AppTasks")]
    public class Task : CommonEntity<int>
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024;//64kb

        [Required, MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public TaskState State { get; set; }

        [ForeignKey(nameof(AssignedPersonId))]
        public User AssignedPerson { get; set; }
        public int? AssignedPersonId { get; set; }

        public Task()
        {
            State = TaskState.Open;
        }
        public Task(string title, string description = null, int? assignedPersonId = null) : this()
        {
            Title = title;
            Description = description;
            AssignedPersonId = assignedPersonId;
        }
    }

    public enum TaskState : byte
    {
        Open = 0,
        Completed = 1
    }
}
