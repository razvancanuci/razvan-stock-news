namespace backend.DataAccess.Entities;

public class Admin : Entity
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public string? Email { get; set; }

    public string? TwoFactorCode { get; set; }
}