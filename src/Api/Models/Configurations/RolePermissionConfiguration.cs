using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFirebase.Api.Models.Domain;
using NetFirebase.Api.Models.Enums;

namespace NetFirebase.Api.Models.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasData(
            Create(Role.Customer, PermissionEnum.ReadUser),
            Create(Role.Customer, PermissionEnum.WriteUser)
        );
    }

    private static RolePermission Create(Role role, PermissionEnum permission)
    {
        return new RolePermission { RoleId = role.Id, PermissionId = (int)permission };
    }
}
