namespace backend.Application.Models;

public class UserIdResponseModel
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public string? Role { get; set; }
}