using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Manager";

    // bire-çok ilişki
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}