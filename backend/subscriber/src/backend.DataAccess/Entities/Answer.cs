namespace backend.DataAccess.Entities;
#nullable disable
public class Answer : Entity
{
    public Guid SubscriberId { get; set; }

    public Subscriber Subscriber { get; set; }
    
    public string OccQuestion { get; set; }
    
    public int AgeQuestion { get; set; }
}