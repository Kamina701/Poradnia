using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Identity.Models;
using Identity.Seed;

namespace Identity
{
    public class SRPIdentityDbContext : IdentityDbContext<SRPUser, SRPRole, Guid, IdentityUserClaim<Guid>, SRPUserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public SRPIdentityDbContext(DbContextOptions<SRPIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SRPUserRole>().HasKey(x => new { x.UserId, x.RoleId });

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tName = entityType.GetTableName();
                if (tName == "IdentityUserClaim<string>") continue;
                entityType.SetTableName(tName.Substring(6));
            }



            builder.Entity<SRPUser>().HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<SRPRole>().HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<SRPUser>().HasMany(x => x.Claims).WithOne().HasForeignKey(x => x.UserId).IsRequired();

            builder.Seed();
        }
    }
}
