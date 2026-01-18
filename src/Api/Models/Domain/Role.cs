namespace NetFirebase.Api.Models.Domain;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Customer = new(1, "Customer");

    public Role(int value, string name)
        : base(value, name) { }

    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<User>? Users { get; set; }
}
