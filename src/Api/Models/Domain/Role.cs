namespace NetFirebase.Api.Models.Domain;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Customer = new(1, "Customer");

    private Role()
        : base(default, string.Empty) { }

    public Role(int id, string name)
        : base(id, name) { }

    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<User>? Users { get; set; }
}
