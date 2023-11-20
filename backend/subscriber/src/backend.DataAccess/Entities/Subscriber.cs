namespace backend.DataAccess.Entities
{
    public class Subscriber : Entity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public Answer? Answer { get; set; }
    }
}
