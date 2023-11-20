namespace backend.DataAccess.Entities
{
    public class Subscriber : EntityGuid
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
