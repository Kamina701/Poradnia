using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity.Models;

namespace Identity.Configurations
{
    public class SRPUserConfiguration : IEntityTypeConfiguration<SRPUser>
    {
        public void Configure(EntityTypeBuilder<SRPUser> builder)
        {
            builder.Ignore(r => r.AccessFailedCount);
            builder.Ignore(r => r.TwoFactorEnabled);
        }
    }
}
