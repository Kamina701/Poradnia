using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using SRP.Models.Seed;
using System.Threading.Tasks;
using System.Threading;
using SRP.interfaces;
using System.Reflection;
using SRP.Services;
using SRP.Models.Enties;

namespace SRP.Models
{
    public class SrpDbContext :  IdentityDbContext<SRPUser, SRPRole, Guid, IdentityUserClaim<Guid>, SRPUserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        public SrpDbContext(DbContextOptions<SrpDbContext> options) : base(options)
        {

        }
        public DbSet<Report> ReportReport { get; set; }
        public DbSet<Access> AccessAccess { get; set; }
        public DbSet<Comment> CommenComment { get; set; }

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

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public Task<int> SaveChangesWithoutUserAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

