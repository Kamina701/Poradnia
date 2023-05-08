using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace SRP.Models.Seed
{
    public static class SeedDefaultUsers
    {
        public static void Seed(this ModelBuilder builder)
        {
            var adminUser = new SRPUser()
            {
                Id = Guid.NewGuid(),
                FirstName = "Wojciech",
                LastName = "Nytko",
                Email = "test@pl.pl",
                NormalizedEmail = "TEST@PL.PL",
                UserName = "TEST@PL.PL",
                NormalizedUserName = "TEST@PL.PL",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            adminUser.PasswordHash = new PasswordHasher<SRPUser>().HashPassword(adminUser, "1qaz@WSX");
            var adminRole = new SRPRole()
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var DoctorRole = new SRPRole()
            {
                Id = Guid.NewGuid(),
                Name = "Doctor",
                NormalizedName = "DOCTOR"
            };

            var unfonfirmedRole = new SRPRole()
            {
                Id = Guid.NewGuid(),
                Name = "Unconfirmed",
                NormalizedName = "UNCONFIRMED"
            };

            var superAdminRole = new SRPRole()
            {
                Id = Guid.NewGuid(),
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            };

            var adminUserRole = new SRPUserRole()
            {
                RoleId = superAdminRole.Id,
                UserId = adminUser.Id
            };

            builder.Entity<SRPUser>().HasData(adminUser);
            builder.Entity<SRPRole>().HasData(adminRole);
            builder.Entity<SRPRole>().HasData(DoctorRole);
            builder.Entity<SRPRole>().HasData(unfonfirmedRole);
            builder.Entity<SRPRole>().HasData(superAdminRole);
            builder.Entity<SRPUserRole>().HasData(adminUserRole);
        }
    }
}
