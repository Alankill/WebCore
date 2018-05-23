using Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityFramework.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
    }
}
