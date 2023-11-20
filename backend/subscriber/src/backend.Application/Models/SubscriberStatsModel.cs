using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class SubscriberStatsModel
    {
        public IEnumerable<int> SubscribedLast7D { get; set; }

        public double PercentageSubscribersWithPhoneNumber { get; set; }
    }
}
