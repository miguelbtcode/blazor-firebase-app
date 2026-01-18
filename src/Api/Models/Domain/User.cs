namespace NetFirebase.Api.Models.Domain;

public sealed class User
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? FirebaseId { get; set; }

    public ICollection<Role>? Roles { get; set; }
}
