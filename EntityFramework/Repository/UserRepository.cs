using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.EntityFramework.IRepository;

namespace Web.EntityFramework.Repository
{
    public class UserRepository:EfRepository<User>,IUserRepository
    {
        public UserRepository(DbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
