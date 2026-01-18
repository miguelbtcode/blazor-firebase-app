using System.ComponentModel.DataAnnotations;

namespace NetFirebase.Api.Models.Domain;

public sealed class User
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? FirebaseId { get; set; }
}
