﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        protected RoleRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
