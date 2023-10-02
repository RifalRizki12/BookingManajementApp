﻿using API.Models;

namespace API.DTOs.AccountRoles
{
    public class CreateAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        // Konversi Implisit (Implicit Conversion):
        // Metode ini akan mengonversi EmployeeDto ke Employee secara implisit jika diperlukan.
        public static implicit operator AccountRole(CreateAccountRoleDto dto)
        {
            return new AccountRole
            {
                AccountGuid = dto.AccountGuid,
                RoleGuid = dto.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
