using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions).WithMany().UsingEntity<RolePermission>();

        builder.HasData(Role.CreateEnumerations().Values.Select(r => new { r.Id, r.Name }));
    }
}
