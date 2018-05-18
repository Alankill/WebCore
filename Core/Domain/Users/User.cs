using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Users
{
    public class User : CommonEntity<int>,IIsDelete
    {
        public const int MaxNameLength = 32;

        [Required,MaxLength(MaxNameLength)]
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public User()
        {

        }

        public User(string name)
        {
            Name = name;
        }
    }
}
