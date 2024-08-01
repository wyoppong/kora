using Kora.Enums;

namespace Kora.Database.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Sex Sex { get; set; }
}