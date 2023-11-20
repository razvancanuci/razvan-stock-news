namespace backend.DataAccess.Entities
{
    public class User : Entity
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public Admin? Admin { get; set; }
    }
}
