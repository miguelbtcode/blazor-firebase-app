using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFirebase.Api.Models.Domain;
using NetFirebase.Api.Models.Enums;

namespace NetFirebase.Api.Models.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);

        IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
            .Select(p => new Permission { Id = (int)p, Name = p.ToString() });

        builder.HasData(permissions);
    }
}
