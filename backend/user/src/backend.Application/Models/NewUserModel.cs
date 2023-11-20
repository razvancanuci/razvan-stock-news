using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class NewUserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
