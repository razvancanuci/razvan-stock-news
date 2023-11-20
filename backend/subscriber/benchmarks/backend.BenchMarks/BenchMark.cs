using BenchmarkDotNet.Attributes;

namespace ConsoleApp1backend.BenchMarks;

[MemoryDiagnoser]
public class BenchMark
{
    [Benchmark]
    public void SubscriberStatsLogic()
    {
        var currentDate = DateTime.UtcNow;
        var last7Days = new List<DateTime>()
        {
            currentDate.AddDays(-6),
            currentDate.AddDays(-5),
            currentDate.AddDays(-4),
            currentDate.AddDays(-3),
            currentDate.AddDays(-2),
            currentDate.AddDays(-1),
            currentDate
        };

        var subscribers = new List<Subscriber>()
        {
            new Subscriber() {SubscriptionDate = new DateTime()},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-1)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-1)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-2)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-3)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-2)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-3)},
            new Subscriber() {SubscriptionDate = DateTime.UtcNow.AddDays(-5)}
        };
        var dates = subscribers.Aggregate(
            new int[] { 0, 0, 0, 0, 0, 0, 0 },
            (list, subscriber) =>
            {
                var correspondingDate = last7Days
                    .FirstOrDefault(day => day.Year == subscriber.SubscriptionDate.Year
                                           && day.Month == subscriber.SubscriptionDate.Month
                                           && day.Day == subscriber.SubscriptionDate.Day);
                var index = last7Days.FindIndex(day => day == correspondingDate);
                    
                if(index >= 0) 
                {
                    list[index]++;
                }

                return list;
            }, list => list
        );
    }

    private class Subscriber
    {
        public DateTime SubscriptionDate { get; set; }
    }
}