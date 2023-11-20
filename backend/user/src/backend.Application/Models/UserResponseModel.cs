using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class UserResponseModel
    {
        public string? Username { get; set; }

        public string? Role { get; set; }
    }
}
