using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class SubscriberDeletedMessage
    {
        public Guid Id { get; init; }
    }
}
